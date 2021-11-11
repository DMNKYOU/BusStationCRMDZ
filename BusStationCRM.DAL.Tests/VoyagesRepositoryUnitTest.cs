using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BusStationCRM.DAL.Tests
{
    public class Tests
    {
        //[SetUp]
        //public void Setup()
        //{
        //    var myDatabaseName = "testDb_" + DateTime.Now.ToFileTimeUtc();

        //    var options = new DbContextOptionsBuilder<AcademyContext>()
        //        .UseInMemoryDatabase(databaseName: myDatabaseName)
        //        .Options;
        //    _context = new AcademyContext(options);
        //    _repository = new CourseCategoryRepository(_context);
        //}

        //[Test]
        //public async Task GetCoursesWithHierarchy_IncludeHierarchy_ReturnsWithHierarchy()
        //{
        //    var category = new CourseCategory { Title = _testCategoryTitle };
        //    _context.CourseCategories.Add(category);
        //    _context.SaveChanges();

        //    var nested = new CourseCategory { Title = "testNestedCategory" };
        //    nested.ParentId = category.Id;
        //    _context.CourseCategories.Add(nested);
        //    _context.SaveChanges();

        //    var course = new Course { Title = "testCourse" };
        //    course.Categories = new[] { category, nested };
        //    _context.Courses.Add(course);
        //    _context.SaveChanges();

        //    var courses = await _repository.GetCoursesByCategory(category.Id);

        //    Assert.AreEqual(1, courses.Count());
        //}

        //[Test]
        //public async Task GetCoursesWithHierarchy_NotIncludeHierarchy_ReturnsWithoutHierarchy()
        //{
        //    //arrange
        //    GenerateTestData();

        //    var testCategory = _context.CourseCategories.Single(c => c.Title == _testCategoryTitle);

        //    //act
        //    var courses = await _repository.GetCoursesByCategory(testCategory.Id, true);

        //    //assert
        //    Assert.AreEqual(2, courses.Count());
        //}

        //private void GenerateTestData()
        //{
        //    var category = new CourseCategory { Title = _testCategoryTitle };
        //    _context.CourseCategories.Add(category);
        //    _context.SaveChanges();

        //    var nested = new CourseCategory
        //    {
        //        Title = "testNestedCategory",
        //        ParentId = category.Id
        //    };
        //    _context.CourseCategories.Add(nested);
        //    _context.SaveChanges();

        //    var course1 = new Course
        //    {
        //        Title = "testCourse1",
        //        Categories = new[] { category }
        //    };
        //    _context.Courses.Add(course1);

        //    var course2 = new Course
        //    {
        //        Title = "testCourse2",
        //        Categories = new[] { nested }
        //    };
        //    _context.Courses.Add(course2);
        //    _context.SaveChanges();
        //}

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}