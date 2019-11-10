using Example_04.Homework.Models;

namespace Example_04.Homework.Clients
{
    public class UserClient
    {
        private IOrmAdapter _ormAdapter;

        public (DbUserEntity, DbUserInfoEntity) Get(int userId)
        {
            return _ormAdapter.Get(userId);
        }

        public void Add(DbUserEntity user, DbUserInfoEntity userInfo)
        {
            _ormAdapter.Add(user, userInfo);
        }

        public void Remove(int userId)
        {
            _ormAdapter.Remove(userId);
        }

        public UserClient(IOrmAdapter ormAdapter)
        {
            _ormAdapter = ormAdapter;
        }
    }
}
