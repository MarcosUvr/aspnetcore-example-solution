using Empresa.Proyecto.Core.Entities;
using Empresa.Proyecto.Core.Interfaces;
using Empresa.Proyecto.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Proyecto.Web.Pages.Catalogos
{
    public class NewEntityModel : BaseCatalogPageModel<NewEntity>
    {
        private readonly IAsyncRepository<SimpleEntity> _simpleEntityRepo;
        private readonly MyProjectContext _context;

        [BindProperty]
        public string Nombre { get; set; } = null!;

        [BindProperty]
        public int OptionId { get; set; }

        public List<SimpleEntity> SimpleEntities { get; set; } = new();

        public NewEntityModel(
            ILogger<BaseCatalogPageModel<NewEntity>> logger, IAsyncRepository<NewEntity> newEntityRepo, IAsyncRepository<SimpleEntity> simpleEntityRepo, MyProjectContext context) : base(logger, newEntityRepo)
        {
            _simpleEntityRepo = simpleEntityRepo;
            _context = context;
        }

        public async Task<JsonResult> OnPostCatalogEntities()
        {
            var catalog = await _context.NewEntity
                .Include(e => e.Option)
                .ToListAsync();

            return new JsonResult(new { data = catalog });
        }

        public async Task OnGetAsync()
        {
            SimpleEntities = (await _simpleEntityRepo.ListAllAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                SimpleEntities = (await _simpleEntityRepo.ListAllAsync()).ToList();
                return Page();
            }

            var selectedOption = await _simpleEntityRepo.GetbyIdAsync(OptionId);
            if (selectedOption == null)
            {
                ModelState.AddModelError("", "La opción seleccionada no es válida.");
                SimpleEntities = (await _simpleEntityRepo.ListAllAsync()).ToList();
                return Page();
            }

            var newEntity = new NewEntity
            {
                Name = Nombre,
                Option = selectedOption,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            };

            await Repo.AddAsync(newEntity);
            return RedirectToPage("/Catalogos/NewEntity");
        }
    }
}
