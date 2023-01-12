using Application.Services;
using Application.ViewModels;
using Data.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class FileController : Controller
    {
        private FileService Service;
        private TextFileDBrepository textFileDBrepository;
        private IWebHostEnvironment webHostEnvironment;
        public FileController(FileService _fileService, TextFileDBrepository textFileDBrepository, IWebHostEnvironment _webHostEnvironment)
        {
            Service = _fileService;
            this.textFileDBrepository = textFileDBrepository;
            this.webHostEnvironment = _webHostEnvironment;
        }
        public IActionResult List()
        {
            var list = Service.GetFilesEntry();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TextFileModel file, IFormFile path)
        {
            try
            {
                //Upload of file
                if (path != null)
                {//C:\Users\User\Desktop\Enterprise\EpHomeAssignment\WebApplication1\Data\
                    string guidFileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(path.FileName);
                    string absolutePath = webHostEnvironment.ContentRootPath + @"\Data\" + guidFileName;
                    using (var destinationFile = System.IO.File.Create(absolutePath))
                    {
                        path.CopyTo(destinationFile);
                    }
                    file.Path = "/Data/" + guidFileName;
                    string[] lines = System.IO.File.ReadAllLines(absolutePath);
                    file.Data = string.Join("", lines);
                }
                var create = Service.CreateNewFile(Guid.NewGuid(), file.Data, file.AuthorName, file.Path);
                ViewBag.Message = "File was created successfully";
                Service.CreatePermissions(create, file.AuthorName, true);

            }
            catch (Exception e)
            {
                ViewBag.Error = "File was not created please check inputs";
            }
            return View();
        }


        [HttpGet]
        public IActionResult EditFile(Guid FileID)
        {
            var ogFile = Service.GetFileEntry(FileID);
            CreateViewModel CreateViewModel = new CreateViewModel()
            {
                FileName = ogFile.FileName,
                Data = ogFile.Data
            };
            return View(CreateViewModel);
        }
        [HttpPost]

        public IActionResult EditFile(Guid FileID, string changedata, CreateViewModel createView)
        {
            try
            {
                if (textFileDBrepository.GetPermissions().Where(x => x.FileName == FileID && x.UserName == User.Identity.Name && x.Permissions == true).FirstOrDefault() != null)
                {
                    createView.FileName = FileID;
                    createView.LastEditedBy = User.Identity.Name;
                    changedata = createView.Data;
                    
                    Service.EditFile(FileID, changedata, createView);
                }
                else
                {
                    throw new Exception("No permission to edit");

                }
            }

            catch (Exception ex)
            {
                
                ViewBag.Error = "File was not updated";
               
            }
            return RedirectToAction("List");
        }
    
    }
}
