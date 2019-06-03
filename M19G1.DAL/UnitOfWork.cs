using M19G1.DAL.Entities;
using M19G1.DAL.Persistence;
using System;
using System.Data;
using System.Data.Entity;

namespace M19G1.DAL
{
    public class UnitOfWork : IDisposable
    {
        private readonly M19G1Context _context = new M19G1Context();

        private DbContextTransaction _transaction;

        private bool _disposed;

        #region AspNetRoles Repository

        private BaseRepository<AspNetRole> _aspNetRolesRepository;

        public BaseRepository<AspNetRole> AspNetRolesRepository =>
            _aspNetRolesRepository ?? (_aspNetRolesRepository = RepositoryFactory.CreateRepository<AspNetRole>(_context));

        #endregion

        #region AspNetUsers Repository

        private BaseRepository<AspNetUser> _aspNetUsersRepository;

        public BaseRepository<AspNetUser> AspNetUsersRepository =>
            _aspNetUsersRepository ?? (_aspNetUsersRepository = RepositoryFactory.CreateRepository<AspNetUser>(_context));

        #endregion

        #region AspNetUserClaims Repository

        private BaseRepository<AspNetUserClaim> _aspNetUserClaimsRepository;

        public BaseRepository<AspNetUserClaim> AspNetUserClaimsRepository =>
            _aspNetUserClaimsRepository ?? (_aspNetUserClaimsRepository = RepositoryFactory.CreateRepository<AspNetUserClaim>(_context));

        #endregion

        #region AspNetUserLogins Repository

        private BaseRepository<AspNetUserLogin> _aspNetUserLoginsRepository;

        public BaseRepository<AspNetUserLogin> AspNetUserLoginsRepository =>
            _aspNetUserLoginsRepository ?? (_aspNetUserLoginsRepository = RepositoryFactory.CreateRepository<AspNetUserLogin>(_context));

        #endregion
        #region Room Repository

        private BaseRepository<Room> _roomRepository;

        public BaseRepository<Room> RoomRepository =>
            _roomRepository ?? (_roomRepository = RepositoryFactory.CreateRepository<Room>(_context));

        #endregion
        #region Category Repository

        private BaseRepository<RoomCategory> _categoryRoomRepository;

        public BaseRepository<RoomCategory> CategoryRoomRepository =>
            _categoryRoomRepository ?? (_categoryRoomRepository = RepositoryFactory.CreateRepository<RoomCategory>(_context));

        #endregion
        #region Facility Repository

        private BaseRepository<Facility> _facilityRepository;

        public BaseRepository<Facility> FacilityRepository =>
            _facilityRepository ?? (_facilityRepository = RepositoryFactory.CreateRepository<Facility>(_context));

        #endregion

        #region RoomFacility Repository

        private BaseRepository<RoomFacility> _roomFacilityRepository;

        public BaseRepository<RoomFacility> RoomFacilityRepository =>
            _roomFacilityRepository ?? (_roomFacilityRepository  = RepositoryFactory.CreateRepository<RoomFacility>(_context));

        #endregion

        #region ExtraFacility Repository

        private BaseRepository<ExtraFacility> _extraFacilitiesRepository;

        public BaseRepository<ExtraFacility> ExtraFacilityRepository =>
            _extraFacilitiesRepository ?? (_extraFacilitiesRepository = RepositoryFactory.CreateRepository<ExtraFacility>(_context));

        #endregion

        #region Booking Repository

        private BaseRepository<Booking> _bookingRepository;

        public BaseRepository<Booking> BookingRepository =>
            _bookingRepository ?? (_bookingRepository = RepositoryFactory.CreateRepository<Booking>(_context));

        #endregion
        #region BookingRoom Repository

        private BaseRepository<BookingRoom> _bookingRoomRepository;

        public BaseRepository<BookingRoom> BookingRoomRepository =>
            _bookingRoomRepository ?? (_bookingRoomRepository = RepositoryFactory.CreateRepository<BookingRoom>(_context));

        #endregion
        #region AnonymousRequest Repository

        private BaseRepository<AnonymousRequest> _anonymousRequestRepository;

        public BaseRepository<AnonymousRequest> AnonymousRequestRepository =>
            _anonymousRequestRepository ?? (_anonymousRequestRepository = RepositoryFactory.CreateRepository<AnonymousRequest>(_context));

        #endregion
        #region UserRequest Repository

        private BaseRepository<UserRequest> _userRequestRepository;

        public BaseRepository<UserRequest> UserRequestRepository =>
            _userRequestRepository ?? (_userRequestRepository = RepositoryFactory.CreateRepository<UserRequest>(_context));

        #endregion

        #region Rating Repository

        private BaseRepository<Rating> _ratingRepository;

        public BaseRepository<Rating> RatingRepository =>
            _ratingRepository ?? (_ratingRepository = RepositoryFactory.CreateRepository<Rating>(_context));

        #endregion


        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void CommitTransaction()
        {
            try
            {
                _transaction?.Commit();
            }
            catch
            {
                _transaction?.Rollback();
                throw;
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _transaction?.Rollback();
            }
            catch
            {
                //Do nothing
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
