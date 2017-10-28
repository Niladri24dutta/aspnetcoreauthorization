using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationLab
{
    public class DocumentRepository : IDocumentRepository
    {
        static List<Document> _documents = new List<Document>
        {
            new Document {Id = 1, Author="Niladri" },
            new Document { Id = 2,Author = "Amarjit" }
        };
        public Document GetDocumentById(int id)
        {
            return _documents.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Document> GetDocuments()
        {
            return _documents;
        }
    }
}
