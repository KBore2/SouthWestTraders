using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouthWestTradersAPI.Domain.Models
{
    public class Response<TEntity>
    {
        public TEntity? Model { get; set; }

        public string Message { get; set; } = string.Empty;

        public bool HasError { get; set; }


        public Response(TEntity? Model, string Message, bool HasError  )
        {
            this.Model = Model;
            this.Message = Message;
            this.HasError = HasError;
        }


    }
}
