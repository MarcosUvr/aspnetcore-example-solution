﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Empresa.Proyecto.Core.Entities;
using Empresa.Proyecto.Core.Interfaces;

namespace Empresa.Proyecto.Web.Pages
{
    public class BaseCatalogPageModel<T>: PageModel where T : BaseEntity
    {
        private protected IAsyncRepository<T> Repo;
        private readonly ILogger<BaseCatalogPageModel<T>> Logger;

        public IReadOnlyList<T> Entidades { get; set; } = null!;

        public BaseCatalogPageModel(ILogger<BaseCatalogPageModel<T>> logger, IAsyncRepository<T> repo)
        {
            Repo = repo;
            Logger = logger;
        }

        public async Task<JsonResult> OnPostCatalog()
        {
            var catalog = await Repo.ListAllAsync();
            return new JsonResult(new { data = catalog });
        }
        public async Task<JsonResult> OnPostCatalogFiltered(int start = 0, int length = 10)
        {
            var (items, totalRecords) = await Repo.GetPagedAsync(start, length);

            return new JsonResult(new
            {
                draw = Request.Form["draw"],
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = items
            });
        }

    }
}
