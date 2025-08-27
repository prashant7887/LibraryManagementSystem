using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.IRepository
{
    public interface IMaster
    {
        public Task<string> saveHour(Hour hour);
        public Task<List<Hour>> GetHourdetails();
        public Task<string> DeleteHour(int id);
        public Task<string> saveCourse(course smm);
        public Task<List<course>> getCourseDetails();
        public Task<string> DeleteCourse(int id);
        public Task<string> saveMembership(Membership mm);
        public Task<List<Membership>> getMembershipDetails();

    }
}
