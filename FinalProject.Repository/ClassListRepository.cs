using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FinalProject.Database;


namespace FinalProject.Repository
{
    public interface IClassListRepository
    {
        List<ClassModel> ClassList();
        List<ClassModel> EnrollInClass();
        EnrollClassModel AddClass(int selectedClassId, int userId);
        List<ClassModel> StudentClasses(int userId);
    }

    public class EnrollClassModel
    {
        public int UserId;
        public int SelectedClassId;
    }
    public class ClassModel
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public decimal ClassPrice { get; set; }
    }
    
    public class ClassListRepository : IClassListRepository
    {
        public List<ClassModel> ClassList()
        {
            var classes = DatabaseAccessor.Instance.Classes.Select(t => new ClassModel
            {
                ClassId = t.ClassId,
                ClassName = t.ClassName,
                ClassDescription=t.ClassDescription,
                ClassPrice=t.ClassPrice
            }).ToList();
            return classes;
        }

        public List<ClassModel> EnrollInClass()
        {
            return null;
        }

        public EnrollClassModel AddClass(int selectedClassId, int userId)
        {
            try
            {
                var user = DatabaseAccessor.Instance.Users.Find(userId);
                var cls = DatabaseAccessor.Instance.Classes.Find(selectedClassId);

                user.Classes.Add(cls);
                //var selectClass = DatabaseAccessor.Instance.Classes.Add(new FinalProject.Database.Class { ClassId=selectedClassId });

                DatabaseAccessor.Instance.SaveChanges();

                return new EnrollClassModel { SelectedClassId = selectedClassId };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ClassModel> StudentClasses(int userId)
        {

            var user = DatabaseAccessor.Instance.Users.Find(userId);
            var sClasses = user.Classes.Select(t => new ClassModel
            {
                ClassId = t.ClassId,
                ClassName = t.ClassName,
                ClassDescription = t.ClassDescription,
                ClassPrice = t.ClassPrice
            }).ToList();

            return sClasses;
        }
    }
}
