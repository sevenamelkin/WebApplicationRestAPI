using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace WebApplicationRestAPI
{
    public class User
    {
        public int Id { get; set; }

        private string _name;

        private string _lastName;

        private DateTime _birthDate;

        public List<Award> _listaward = new List<Award>();

        public User()
        {
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Length < 50)
                {
                    _name = value;
                }
                else
                {
                    throw new Exception("user name can't be empty or overly long!");
                }
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Length < 50)
                {
                    _lastName = value;
                }
                else
                {
                    throw new Exception("last name user can't be empty or everly long!");
                }

            }
        }

        public DateTime BirthDate
        {
            get
            {
                return _birthDate;
            }
            set
            {
                if (value > DateTime.Now.AddYears(-150) & value < DateTime.Now)
                {
                    _birthDate = value;
                }
                else
                {
                    throw new Exception("user can not be born in the future or be so old");
                }
            }
        }

        public int Age
        {
            get
            {
                return CountFullYears(BirthDate);
            }
        }

        public int CountFullYears(DateTime date)
        {
            var currentDate = DateTime.Now;
            int range = currentDate.Year - date.Year;
            if (currentDate.DayOfYear < date.DayOfYear)
            {
                range--;
            }
            return range;
        }

        public List<Award> Listawards
        {
            get
            {
                return _listaward;
            }

            set
            {
                _listaward = value;
            }
        }
        /*public void AddAwardToListAwardUser(Award award) //WebApplicationRestAPI.Award
        {
            var result = Convert.ToInt32(award.Id);
            _listaward.Add(award[result]);
        }*/
        /*var result = Convert.ToInt32(txtAward.Text);
        result--;
                if (user._listaward.Contains(AwardsDAO.awards[result]))
                {
                    MessageBox.Show("This award has already been awarded");
                }
                else
                {
                    user._listaward.Add(AwardsDAO.awards[result]);
                }*/
        public string Award
        {
            get
            {
                return CountTitle();
            }
        }

        public string CountTitle()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var listr in _listaward)
            {
                sb.Append(listr.Title + " ");
            }
            return sb.ToString();
        }
    }
}
