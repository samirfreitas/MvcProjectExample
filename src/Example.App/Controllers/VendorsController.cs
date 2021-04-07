using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Example.App.ViewsModels;
using Example.Business.Intefaces;
using AutoMapper;
using Example.Business.Models;
using System.Collections.Generic;

namespace Example.App.Controllers
{
    [Route("Fornecedor")]
    public class VendorsController : BaseController
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly IVendorServices _vendorService;
        private readonly IMapper _mapper;

        public VendorsController(IVendorRepository vendorRepository, IMapper mapper, IVendorServices vendorService, INotificator notificator) : base(notificator)
        {
            _vendorRepository = vendorRepository;
            _mapper = mapper;
            _vendorService = vendorService;
        }
      
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<VendorViewModel>>(await _vendorRepository.GetAll()));
        }
        [Route("Detalhes")]
        public async Task<IActionResult> Details(Guid id)
        {

            var vendorViewModel = await GetVendorWithAddress(id);
            if (vendorViewModel == null)
            {
                return NotFound();
            }

            return View(vendorViewModel);
        }

        [Route("Cadastrar")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Cadastrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VendorViewModel vendorViewModel)
        {
            if (ModelState.IsValid)
            {
                var vendor = _mapper.Map<Vendor>(vendorViewModel);
                await _vendorService.Add(vendor);

                if (!ValidOperation()) return View(vendorViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(vendorViewModel);
        }

        [Route("Editar")]
        public async Task<IActionResult> Edit(Guid id)
        {
      
            var vendorViewModel = _mapper.Map<VendorViewModel>(await GetVendorWithAddressAndProducts(id));
            if (vendorViewModel == null)
            {
                return NotFound();
            }
            return View(vendorViewModel);
        }

        [HttpPost("Editar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, VendorViewModel vendorViewModel)
        {
            if (id != vendorViewModel.Id) return NotFound();
            

            if (ModelState.IsValid)
            {
                var vendor = _mapper.Map<Vendor>(vendorViewModel);
                await _vendorService.Update(vendor);
                if (!ValidOperation()) return View(vendorViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(vendorViewModel);
        }


        [HttpGet("Excluir")]
        public async Task<IActionResult> Delete(Guid id)
        {


            var vendorViewModel = await GetVendorWithAddress(id);
            if (vendorViewModel == null)
            {
                return NotFound();
            }

            return View(vendorViewModel);
        }

      
        [HttpPost, ActionName("Excluir")]     
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var vendorViewModel = await GetVendorWithAddress(id);

            if (vendorViewModel == null) return NotFound();

            await _vendorService.Delete(id);
            if (!ValidOperation()) return View(vendorViewModel);

            return RedirectToAction(nameof(Index));
        }

        [Route("ObterEndereco")]
        public async Task<IActionResult> GetAddress(Guid id)
        {
            var vendor = await GetVendorWithAddress(id);
            if (vendor == null)
            {
                return NotFound();
            }

            return PartialView("_AddressDetails", vendor);
        }



        [Route("AtualizarEndereco")]        
        public async Task<IActionResult> AddressUpdate(Guid id)
        {
            var vendor = await GetVendorWithAddress(id);

            if(vendor == null)
            {
                return NotFound();
            }

            return PartialView("_AddressUpdate", new VendorViewModel { Address = vendor.Address });

        }

        [HttpPost("AtualizarEndereco")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddressUpdate(Guid id, VendorViewModel vendorViewModel)
        {

            ModelState.Remove("Name");
            ModelState.Remove("Document");

            if (!ModelState.IsValid) return PartialView("AddressUpdate", vendorViewModel);

            await _vendorService.UpdateAddress(_mapper.Map<Address>(vendorViewModel.Address));

            var url = Url.Action("GetAddress", "Vendors", new { id = vendorViewModel.Address.VendorId });

            return Json(new { success = true, url });          

        }


        private async Task<VendorViewModel> GetVendorWithAddress(Guid id)
        {
            return _mapper.Map<VendorViewModel>(await _vendorRepository.GetVendorWithAddress(id));
        }


        private async Task<VendorViewModel> GetVendorWithAddressAndProducts(Guid id)
        {
            return _mapper.Map<VendorViewModel>(await _vendorRepository.GetVendorWithAddressAndProducts(id));
        }
    }
}
