using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AuthorizationLab.Controllers
{
    
    public class DocumentController : Controller
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IAuthorizationService _authService;

        public DocumentController(IDocumentRepository repository, IAuthorizationService service)
        {
            _documentRepository = repository;
            _authService = service;
        }

        public IActionResult Index()
        {
            return View(_documentRepository.GetDocuments());
        }

        public async Task<IActionResult> Edit(int id)
        {
            var document = _documentRepository.GetDocumentById(id);
            if(document == null)
            {
                return new NotFoundResult();
            }
            if(await _authService.AuthorizeAsync(User,document,new EditRequirement()))
            {
                return View(document);
            }
            else
            {
                return new ChallengeResult();
            }
        }
    }
}
