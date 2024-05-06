using ReasnAPI.Models.Database;
using System.Linq.Expressions;

namespace ReasnAPI.Services
{
    public class ObjectTypeService(ReasnContext context)
    {
        private readonly ReasnContext _context = context;

        public ObjectType? CreateObjectType(ObjectType? objectType)
        {
            if (objectType is null)
                return null;

            // check if object type with the same name already exists
            var objectTypeDb = _context.ObjectTypes.FirstOrDefault(r => r.Name == objectType.Name);

            if (objectTypeDb is not null)
                return null;

            _context.ObjectTypes.Add(objectType);
            _context.SaveChanges();

            return objectType;
        }

        public ObjectType? UpdateObjectType(ObjectType? objectType)
        {
            if (objectType is null)
                return null;

            var objectTypeDb = _context.ObjectTypes.FirstOrDefault(r => r.Id == objectType.Id);

            if (objectTypeDb is null)
                return null;

            objectTypeDb.Name = objectType.Name;
            _context.SaveChanges();

            return objectType;
        }

        // TODO: Refator this method to use bool return type
        public void DeleteObjectType(int objectTypeId)
        {
            var objectType = _context.ObjectTypes.FirstOrDefault(r => r.Id == objectTypeId);

            if ( objectType is null)
                return;

            _context.ObjectTypes.Remove(objectType);
            _context.SaveChanges();
        }

        public ObjectType? GetObjectTypeById(int objectTypeId)
        {
            return _context.ObjectTypes.FirstOrDefault(r => r.Id == objectTypeId);
        }

        public IEnumerable<ObjectType> GetObjectTypesByFilter(Expression<Func<ObjectType, bool>> filter)
        {
            return _context.ObjectTypes
                           .Where(filter)
                           .AsEnumerable();
        }

        public IEnumerable<ObjectType> GetAllObjectTypes()
        {
            return _context.ObjectTypes.AsEnumerable();
        }
    }
}