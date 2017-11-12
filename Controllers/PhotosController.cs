using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AgilFood.Controllers.Resource;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using AgilFood.Core;
using AgilFood.Persistence;
using AgilFood.Core.models;


namespace AgilFood.Controllers
{
    // /api/fornecedores/1/photos
    [Route("/api/fornecedores/{fornecedorId}/photos")]
    public class PhotosController : Controller
    {
        private readonly IHostingEnvironment _host;
        private readonly IFornecedorRepository _repository;
        private readonly IPhotoRepository _photoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PhotoSettings photoSettings;

        public PhotosController(IHostingEnvironment host, IFornecedorRepository repository, IPhotoRepository photoRepository,
            IUnitOfWork unitOfWork, IMapper mapper, IOptionsSnapshot<PhotoSettings> options)
        {
            _host = host;
            _repository = repository;
            _photoRepository = photoRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            this.photoSettings = options.Value;
            //www.root, where I will put all the uploads
        }

        [HttpPost]
        public async Task<IActionResult> Upload(int fornecedorId, IFormFile file)
        {
            var fornecedor = await _repository.GetFornecedor(fornecedorId, includeRelated: false);

            if (fornecedor == null)
                return NotFound();
            
            if (file == null)
                return NotFound("Null File");
            
            if (file.Length == 0)
                return NotFound("Empty File");
            
            if (file.Length > photoSettings.MaxBytes) //maior q 10 Mb, se caso precisar mudar o tamanho, basta ir no appsettings.json
                return NotFound("Max file size exceeded");
            
            if (!photoSettings.IsSupported(file.FileName))
                return NotFound("Invalid file type");
            

            var upladsFolderPath= Path.Combine(_host.WebRootPath, "uploads");

            if (!Directory.Exists(upladsFolderPath)) //Se nao existir essa pasta, simplesmente a criamos
                Directory.CreateDirectory(upladsFolderPath);
            
            //para proteger do usuario upar qualquer tipo de arquivo
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath= Path.Combine(upladsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var photo = new Photo { FileName = fileName };

            fornecedor.Photos.Add(photo);
            await _unitOfWork.CompleteAsync();

            return Ok(_mapper.Map<Photo, PhotoResource>(photo));
        }


        [HttpGet]
        public async Task<IEnumerable<PhotoResource>> GetPhotos(int fornecedorId)
        {
            var photos = await _photoRepository.GetPhotos(fornecedorId);

            return _mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos);
        }
    }
}

