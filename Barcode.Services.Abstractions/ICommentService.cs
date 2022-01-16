using System;
using DataAccess.Daos;

namespace Barcode.Services.Abstracitons
{
    public interface ICommentService
    {
        Comment Get(int id);
        void Create(string data, int rating, int scanId);
        Comment Remove(int id);
        Comment Edit(int id,string data, int rating, int scanId);
    }
}