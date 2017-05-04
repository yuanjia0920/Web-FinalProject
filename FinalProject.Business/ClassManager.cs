using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.Repository;

namespace FinalProject.Business
{
    public interface IClassManager
    {
        List<ClassModel> ClassList();
              
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
    public class ClassManager : IClassManager
    {
        private readonly IClassListRepository classListRepository;

        public ClassManager(IClassListRepository classListRepository)
        {
            this.classListRepository = classListRepository;
        }

        public List<ClassModel> ClassList()
        {

            var classes = classListRepository.ClassList();
            var returnList =  new List<ClassModel>();
            foreach (var c in classes)
            {
                var ret = new ClassModel() { ClassId = c.ClassId,ClassName = c.ClassName,ClassDescription = c.ClassDescription, ClassPrice = c.ClassPrice};
                returnList.Add(ret);
            }
            return returnList;
        }

        public EnrollClassModel AddClass(int selectedClassId, int userId)
        {
            var selectClass = classListRepository.AddClass(selectedClassId,userId);
            return new EnrollClassModel { SelectedClassId=selectClass.SelectedClassId, UserId=selectClass.UserId };
        }

        public List<ClassModel> StudentClasses(int userId)
        {
            var sClasses = classListRepository.StudentClasses(userId);
            var returnList = new List<ClassModel>();
            foreach (var c in sClasses)
            {
                var sc = new ClassModel() { ClassId = c.ClassId, ClassName = c.ClassName, ClassDescription = c.ClassDescription, ClassPrice = c.ClassPrice };
                returnList.Add(sc);
            }
            return returnList;
        }
    }
}
