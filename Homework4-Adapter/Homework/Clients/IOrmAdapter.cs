using Example_04.Homework.FirstOrmLibrary;
using Example_04.Homework.Models;
using Example_04.Homework.SecondOrmLibrary;
using System.Linq;

namespace Example_04.Homework.Clients
{
    public interface IOrmAdapter // ITarget
    {
        (DbUserEntity, DbUserInfoEntity) Get(int userId);
        void Add(DbUserEntity user, DbUserInfoEntity userInfo);
        void Remove(int userId);
    }

    public class FirstOrmAdapter : IOrmAdapter {
        private IFirstOrm<DbUserEntity> _firstOrm1;
        private IFirstOrm<DbUserInfoEntity> _firstOrm2;

        public FirstOrmAdapter(IFirstOrm<DbUserEntity> firstOrm1, IFirstOrm<DbUserInfoEntity> firstOrm2) {
            _firstOrm1 = firstOrm1;
            _firstOrm2 = firstOrm2;
        }

        public (DbUserEntity, DbUserInfoEntity) Get(int userId) {
            var user = _firstOrm1.Read(userId);
            var userInfo = _firstOrm2.Read(user.InfoId);
            return (user, userInfo);
        }

        public void Add(DbUserEntity user, DbUserInfoEntity userInfo) {
            _firstOrm1.Add(user);
            _firstOrm2.Add(userInfo);
        }

        public void Remove(int userId) {
            (var user, var userInfo) = Get(userId);

            _firstOrm2.Delete(userInfo);
            _firstOrm1.Delete(user);
        }
    }

    public class SecondOrmAdapter : IOrmAdapter {
        private ISecondOrm _secondOrm;

        public SecondOrmAdapter(ISecondOrm secondOrm) {
            _secondOrm = secondOrm;
        }

        public (DbUserEntity, DbUserInfoEntity) Get(int userId) {
            var user = _secondOrm.Context.Users.First(i => i.Id == userId);
            var userInfo = _secondOrm.Context.UserInfos.First(i => i.Id == user.InfoId);
            return (user, userInfo);
        }

        public void Add(DbUserEntity user, DbUserInfoEntity userInfo) {
            _secondOrm.Context.Users.Add(user);
            _secondOrm.Context.UserInfos.Add(userInfo);
        }

        public void Remove(int userId) {
            (var user, var userInfo) = Get(userId);

            _secondOrm.Context.Users.Remove(user);
            _secondOrm.Context.UserInfos.Remove(userInfo);
        }
    }
}
