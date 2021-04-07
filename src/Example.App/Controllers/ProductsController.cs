using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Example.App.Data;
using Example.App.ViewsModels;
using Example.Business.Intefaces;
using AutoMapper;
using Example.Business.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Example.App.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly IVendorRepository _vendorRepository;
        private readonly IProductServices _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository,IProductServices productService, IVendorRepository vendorRepository, IMapper mapper, INotificator notificator) : base(notificator)
        {
            _productRepository = productRepository;
            _productService = productService;
            _vendorRepository = vendorRepository;
            _mapper = mapper;
            
        }

      
        public async Task<IActionResult> Index()
        {            
            var products = await _productRepository.GetProductsVendors();

            return View(_mapper.Map<IEnumerable<ProductViewModel>>(products));
        }

        
        public async Task<IActionResult> Details(Guid id)
        {
            var productViewModel = await GetProduct(id);
            
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

       
        public async Task<IActionResult> Create()
        {           
            return View(await PopulateVendors(new ProductViewModel()));
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            productViewModel = await PopulateVendors(productViewModel);

            var imgPrefix = Guid.NewGuid() + "_";
            if(!await UploadFile(productViewModel.ImageUpload, imgPrefix))
            {
                return View(productViewModel);
            }

            productViewModel.Image = imgPrefix + productViewModel.ImageUpload.FileName;

            if (ModelState.IsValid)
            {
                Product entity = _mapper.Map<Product>(productViewModel);
                await _productService.Add(entity);

                if (!ValidOperation()) return View(productViewModel);

                return RedirectToAction(nameof(Index));
            }

            return View(productViewModel);
        }

        private async Task<bool> UploadFile(IFormFile file, string imgPrefix)
        {
            if (file.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgPrefix + file.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Ja Existe um arquivo com este nome!");
                return false;
            }

            using(var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return true;
        }

        public async Task<IActionResult> Edit(Guid id)
        {


            var productViewModel = await GetProduct(id);
   
            if (productViewModel == null)
            {
                return NotFound();
            }
           
            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (productViewModel.ImageUpload != null)
                {
                    var imgPrefix = Guid.NewGuid() + "_";
                    if (!await UploadFile(productViewModel.ImageUpload, imgPrefix))
                    {
                        return View(productViewModel);
                    }
                    productViewModel.Image = imgPrefix + productViewModel.ImageUpload.FileName;                  
                }

                if (string.IsNullOrWhiteSpace(productViewModel.Image)) {
                    var oldProduct = await  GetProduct(id);
                    productViewModel.Image = oldProduct.Image;
                }

                await _productService.Update(_mapper.Map<Product>(productViewModel));

                if (!ValidOperation()) return View(productViewModel);

                return RedirectToAction(nameof(Index));
            }
         
            return View(productViewModel);
        }

 
        public async Task<IActionResult> Delete(Guid id)
        {


            var productViewModel = await GetProduct(id);
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var productViewModel = await GetProduct(id);

            if(productViewModel == null)
            {
                return NotFound();
            }

            await _productService.Delete(id);

            if (!ValidOperation()) return View(productViewModel);

            return RedirectToAction(nameof(Index));
        }


        private async Task<ProductViewModel> GetProduct(Guid id)
        {
            var produto = _mapper.Map<ProductViewModel>(await _productRepository.GetProductWithVendor(id));

            produto.Vendors = _mapper.Map<IEnumerable<VendorViewModel>>(await _vendorRepository.GetAll());

            return produto;
        }



        private async Task<ProductViewModel> PopulateVendors(ProductViewModel produto)
        {            
            produto.Vendors = _mapper.Map<IEnumerable<VendorViewModel>>(await _vendorRepository.GetAll());

            return produto;
        }


    }
}
