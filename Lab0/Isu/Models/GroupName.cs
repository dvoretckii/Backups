using Isu.Exceptions;

namespace Isu.Models;

public class GroupName
{
    private char _faculty;
    private char _studyForm;
    private CourseNumber _courseNumber;
    private string _groupNumber;

    public GroupName()
    {
        _faculty = '\0';
        _studyForm = '\0';
        _courseNumber = new CourseNumber();
        _groupNumber = string.Empty;
    }

    public GroupName(char faculty, char studyForm, CourseNumber courseNumber, string groupNumber)
    {
        const string lettersAlphabet = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";
        const string numbersAlphabet = "1234567890";
        if (!lettersAlphabet.Contains(faculty))
        {
            throw new GroupNameException("No such faculty");
        }

        if (!numbersAlphabet.Contains(studyForm))
        {
            throw new GroupNameException("No such study form");
        }

        foreach (char ch in groupNumber)
        {
            if (!numbersAlphabet.Contains(ch))
            {
                throw new GroupNameException("Group should be number");
            }
        }

        _faculty = faculty;
        _studyForm = studyForm;
        _courseNumber = courseNumber;
        _groupNumber = groupNumber;
    }

    public string GetGroupName()
    {
        string name = _faculty + _studyForm + _courseNumber.GetCourseNumber() + _groupNumber;
        return name;
    }

    public CourseNumber GetCourseNumber()
    {
        CourseNumber courseNumber = _courseNumber;
        return courseNumber;
    }
}