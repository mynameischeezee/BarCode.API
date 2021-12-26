using System;
using System.IO;

namespace DataAccess.Resource
{
    public static class ResourceHelper
    {
        public static string GetDbLocation()
        {
            var d = Directory.GetParent(Directory.GetCurrentDirectory());
            return $"{d}/DataAccess/Resource/Barcode.db";
        }
         
    }
}