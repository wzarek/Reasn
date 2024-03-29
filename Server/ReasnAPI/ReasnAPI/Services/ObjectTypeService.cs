using ReasnAPI.Models.Database;
using System.Linq.Expressions;

namespace ReasnAPI.Services {
    public class ObjectTypeService (ReasnContext context) {
        private readonly ReasnContext _context = context;

        public ObjectType CreateObjectType(ObjectType objectType) {
            _context.ObjectTypes.Add(objectType);
            _context.SaveChanges();

            return objectType;
        }

        public ObjectType UpdateObjectType(ObjectType objectType) {
            _context.ObjectTypes.Update(objectType);
            _context.SaveChanges();
            
            return objectType;
        }

        public void DeleteObjectType(int objectTypeId) {
            var objectType = _context.ObjectTypes.FirstOrDefault(r => r.Id == objectTypeId);

            if (objectType != null) {
                _context.ObjectTypes.Remove(objectType);
                _context.SaveChanges();
            }
        }

        public ObjectType GetObjectTypeById(int objectTypeId) {
            return _context.ObjectTypes.FirstOrDefault(r => r.Id == objectTypeId);
        }

        public List<ObjectType> GetObjectTypesByFilter(Expression<Func<ObjectType, bool>> filter) {
            return _context.ObjectTypes.Where(filter).ToList();
        }

        public List<ObjectType> GetAllObjectTypes() {
            return _context.ObjectTypes.ToList();
        }
    }
}
