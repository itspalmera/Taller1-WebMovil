using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;

namespace Taller1_WebMovil.Src.Interface
{
    public class IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile formFile);
    }
}