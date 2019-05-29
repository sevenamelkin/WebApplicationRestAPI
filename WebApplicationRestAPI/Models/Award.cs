using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationRestAPI
{
    public class Award
    {
        private string _title;
        private string _description;
        public int Id { get; set; }

        public Award()
        {
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length < 50)
                {
                    _title = value;
                }
                else
                {
                    throw new Exception("title is too long or empty!");
                }
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length < 150)
                {
                    _description = value;
                }
                else
                {
                    throw new Exception("description is too long or empty");
                }
            }
        }
    }
}
