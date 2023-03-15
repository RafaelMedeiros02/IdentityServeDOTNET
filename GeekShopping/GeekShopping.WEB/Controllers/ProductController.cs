using GeekShopping.WEB.Models;
using GeekShopping.WEB.Services.IServices;
using GeekShopping.WEB.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.WEB.Controllers
{


    public class ProductController : Controller
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [Authorize]
        //EXIBE PRODUTOS, POPULANDO TABELA 
        public async Task <IActionResult> ProductIndex()
        {
            var products = await _productService.FindAllProducts();
            return View(products);
        }

        //RETORNA VIEW COMO ESTÁ  
        public async Task<IActionResult> ProductCreate()
        {
           
            return View();
        }

        //CREATE NEW PRODUCT 
        //ADICIONANDO AUTHORIZE PARA AUTENTICAR OS PERFIS QUE TEM ACESSO
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProduct(model);
                if (response != null) return RedirectToAction(nameof(ProductIndex));
            }
            return View(model);
        }




        //RETORNA VIEW COMO ESTÁ  
        public async Task<IActionResult> ProductUpdate(int id)
        {
            var model = await _productService.FindProductById(id);
            if (model != null) return View(model);

            return NotFound();
        }

        //UPDATE PRODUCT
        //ADICIONANDO AUTHORIZE PARA AUTENTICAR OS PERFIS QUE TEM ACESSO
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProduct(model);
                if (response != null) return RedirectToAction(nameof(ProductIndex));
            }
            return View(model);
        }

        //RETORNA VIEW COMO ESTÁ  
        public async Task<IActionResult> ProductDelete(int id)
        {
            var model = await _productService.FindProductById(id);
            if (model != null) return View(model);

            return NotFound();
        }

        //DELETE PRODUCT
        //ADICIONANDO AUTHORIZE PARA AUTENTICAR OS PERFIS QUE TEM ACESSO
       
        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ProductDelete(ProductModel model)
        {
         
                var response = await _productService.DeleteProductById(model.Id);
                if (response) return RedirectToAction(nameof(ProductIndex));
            
            return View(model);
        }


    }
}
