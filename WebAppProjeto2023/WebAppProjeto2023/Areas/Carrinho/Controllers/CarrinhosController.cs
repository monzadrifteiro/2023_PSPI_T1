﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo.Carrinho;
using Servico.Cadastros;
using Servico.Carrinho;

namespace WebAppProjeto2023.Areas.Carrinho.Controllers
{
    public class CarrinhosController : Controller
    {
        ProdutoServico produtoServico = new ProdutoServico();
        CarrinhoServico carrinhoServico = new CarrinhoServico();
        public ActionResult Create()
        {
            IEnumerable<ItemCarrinho> carrinho =
            HttpContext.Session["carrinho"] as IEnumerable<ItemCarrinho>;
            if (carrinho == null)
            {

                carrinho = new List<ItemCarrinho>();
                HttpContext.Session["carrinho"] = carrinho;

            }
            PopularViewBag();
            return View(carrinho);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            IEnumerable<ItemCarrinho> carrinho = HttpContext.Session["carrinho"] as
            IEnumerable<ItemCarrinho>;
            if (carrinho == null)
            {
                carrinho = new List<ItemCarrinho>();
                HttpContext.Session["carrinho"] = carrinho;
            }
            // Cadastra Pedido
            DateTime d = DateTime.Now;
            foreach (ItemCarrinho itemCarrinho in carrinho)
            {
                long produtoId = (long)itemCarrinho.Produto.ProdutoId;
                var itemPedido = new ItemPedido()
                {
                    Usuario = "Teste",
                    Data = d,
                    ProdutoId = produtoId,
                    Quantidade = itemCarrinho.Quantidade,
                    ValorUnitario = itemCarrinho.ValorUnitario
                };
                carrinhoServico.GravarItemPedido(itemPedido);
            }
            // Limpa Carrinho
            carrinho = new List<ItemCarrinho>();
            HttpContext.Session["carrinho"] = carrinho;
            return RedirectToAction("Create");
        }
        private void PopularViewBag()
        {
            ViewBag.ItemCarrinhoId = new
            SelectList(produtoServico.ObterProdutosClassificadosPorNome(),
            "ProdutoId", "Nome");
        }
        public ActionResult AddProduto(FormCollection collection)
        {
            List<ItemCarrinho> carrinho = HttpContext.Session["carrinho"] as
            List<ItemCarrinho>;
            var produto2 = produtoServico.ObterProdutoPorId(
            Convert.ToInt32(collection.Get("ItemCarrinhoId")));
            var qtd = Convert.ToInt32(collection.Get("Quantidade"));
            var itemCarrinho = new ItemCarrinho()
            {
                Produto = produto2,
                Quantidade = qtd,
                ValorUnitario = 1
            };
            carrinho.Add(itemCarrinho);
            HttpContext.Session["carrinho"] = carrinho;
            return RedirectToAction("Create");
        }
    }
}