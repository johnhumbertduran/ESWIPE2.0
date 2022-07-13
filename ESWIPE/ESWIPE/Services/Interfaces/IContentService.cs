using System;
using System.Collections.Generic;
using System.Text;

using ESWIPE.Models;
using System.Threading.Tasks;

namespace ESWIPE.Services.Interfaces
{
    public interface IContentService
    {
        Task<bool> AddorUpdateContent(ContentModel contentModel);
        Task<bool> DeleteContent(string key);
        Task<List<ContentModel>> GetAllContent();
        Task<List<ContentModel>> GetAllContentQ1();
        Task<List<ContentModel>> GetAllContentQ2();
        Task<List<ContentModel>> GetAllContentQ3();
        Task<List<ContentModel>> GetAllContentQ4();
    }
}
