using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FixtureFormatter
{
    class Program
    {
        #region Global Variables
        public static ConsoleKeyInfo userResponseKey;
        public static String userResponse;

        public static List<string> inputDay = new List<string>();
        public static List<string> inputDate = new List<string>();
        public static List<string> inputCourse = new List<string>();
        public static List<string> inputCode = new List<string>();
        public static List<string> inputSurface = new List<string>();
        public static List<string> inputSession = new List<string>();
        public static List<string> inputType = new List<string>();
        #endregion

        public static int numberOfLines = 0;

        static void writeLine(string text)
        {
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (text.Length / 2)) + "}", text));
        }

        static void write(string text)
        {
            Console.Write(String.Format("{0," + ((Console.WindowWidth / 2) + (text.Length / 2)) + "}", text));
        }

        static void Main(string[] args)
        {

            writeLine("-*-*-*-*-*-*-*-*-*-*-  Racing Profits Guides - Fixture Importer/Formatter  -*-*-*-*-*-*-*-*-*-*-");
            writeLine("");
            writeLine("Instructions:");
            writeLine("File must have been downloaded from www.britishhorseracing.com/resource-centre/fixture-list/");
            writeLine("If the file has been changed over previous years, please contact the developer at Jack_Duffy@outlook.com");
            writeLine("Input file must be in CSV format");
            writeLine("Input file must be made of the following columns - DAY, DATE, COURSE, CODE, SURFACT, SESSION, TYPE");
            writeLine("");
            writeLine("Verification:");
            writeLine("Please ensure that the file format is named 'FIXTURES_LIST' and it is located on the desktop.");
            writeLine("Are you ready to proceed?");
            userResponseKey = Console.ReadKey();
            userResponse = userResponseKey.KeyChar.ToString();
            Console.Write("\b");

            if (userResponse == "Y" || userResponse == "y")
            {
                readFile();
            }

            Console.ReadLine();
        }

        static void readFile()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "FIXTURES_LIST.csv");
            bool fileReadSuccessful = false;

            numberOfLines = 0;
            writeLine("");
            writeLine("Reading File, please wait.");
            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        inputDay.Add(values[0]);
                        inputDate.Add(values[1]);
                        inputCourse.Add(values[2]);
                        inputCode.Add(values[3]);
                        inputSurface.Add(values[4]);
                        inputSession.Add(values[5]);
                        inputType.Add(values[6]);
                        numberOfLines++;
                    }
                }

                fileReadSuccessful = true;
            }

            catch (Exception e)
            {
                writeLine("");
                writeLine("Critical Error!");
                writeLine("Please check that the file is named 'FIXTURES_LIST.csv', located on the Desktop and no other software is currently accessing it");
                writeLine("Would you like to view the full error message?");
                userResponseKey = Console.ReadKey();
                userResponse = userResponseKey.KeyChar.ToString();
                Console.Write("\b");

                if (userResponse == "Y" || userResponse == "y")
                {
                    Console.WriteLine(e);
                }

                fileReadSuccessful = false;
            }

            if (fileReadSuccessful == true)
            {
                writeLine("File successfully read.");
                writeLine("");
                writeLine("Would you like to limit the output to a specific month?");
                writeLine("Y/N");
                userResponseKey = Console.ReadKey();
                userResponse = userResponseKey.KeyChar.ToString();
                Console.Write("\b");

                if (userResponse == "Y" || userResponse == "y")
                {
                    writeLine("");
                    writeLine("What month? (numerical, i.e. January = 01)");
                    write("Month:   ");
                    Console.Write("\b\b");
                    string targetMonth = Console.ReadLine();

                    List<string> tempDay = new List<string>();
                    List<string> tempDate = new List<string>();
                    List<string> tempCourse = new List<string>();
                    List<string> tempCode = new List<string>();
                    List<string> tempSurface = new List<string>();
                    List<string> tempSession = new List<string>();
                    List<string> tempType = new List<string>();
                    int tempNumberOfLines = 0;
                    for (int i = 0; i < numberOfLines; i++)
                    {
                        string[] dateElements = inputDate[i].Split('/');
                        if (dateElements[1] == targetMonth)
                        {
                            tempDay.Add(inputDay[i]);
                            tempDate.Add(inputDate[i]);
                            tempCourse.Add(inputCourse[i]);
                            tempCode.Add(inputCode[i]);
                            tempSurface.Add(inputSurface[i]);
                            tempSession.Add(inputSession[i]);
                            tempType.Add(inputType[i]);
                            tempNumberOfLines++;
                        }
                    }

                    inputDay = new List<string>();
                    inputDate = new List<string>();
                    inputCourse = new List<string>();
                    inputCode = new List<string>();
                    inputSurface = new List<string>();
                    inputSession = new List<string>();
                    inputType = new List<string>();
                    numberOfLines = tempNumberOfLines;

                    inputDay = tempDay;
                    inputDate = tempDate;
                    inputCourse = tempCourse;
                    inputCode = tempCode;
                    inputSurface = tempSurface;
                    inputSession = tempSession;
                    inputType = tempType;

                    string monthName = "";
                    switch(targetMonth)
                    {
                        case "01":
                            monthName = "January";
                            break;
                        case "02":
                            monthName = "February";
                            break;
                        case "03":
                            monthName = "March";
                            break;
                        case "04":
                            monthName = "April";
                            break;
                        case "05":
                            monthName = "May";
                            break;
                        case "06":
                            monthName = "June";
                            break;
                        case "07":
                            monthName = "July";
                            break;
                        case "08":
                            monthName = "August";
                            break;
                        case "09":
                            monthName = "September";
                            break;
                        case "10":
                            monthName = "October";
                            break;
                        case "11":
                            monthName = "November";
                            break;
                        case "12":
                            monthName = "December";
                            break;
                    }

                    writeLine("Success. Content has been filtered for " + monthName);
                }

                writeLine("");
                writeLine("Would you like to exclude any months?");
                writeLine("Y/N");
                userResponseKey = Console.ReadKey();
                userResponse = userResponseKey.KeyChar.ToString();
                Console.Write("\b");

                if (userResponse == "Y" || userResponse == "y")
                {
                    writeLine("Which months would you want to exclude? (seperate by a - )");
                    string userExclusions = Console.ReadLine();
                    string[] monthsToExclude = userExclusions.Split(',');

                    List<string> tempDay = new List<string>();
                    List<string> tempDate = new List<string>();
                    List<string> tempCourse = new List<string>();
                    List<string> tempCode = new List<string>();
                    List<string> tempSurface = new List<string>();
                    List<string> tempSession = new List<string>();
                    List<string> tempType = new List<string>();
                    int tempNumberOfLines = 0;


                    for (int i = 0; i < numberOfLines; i++)
                    {
                        string[] dateElements = inputDate[i].Split('/');

                        if (monthsToExclude.Contains(dateElements[1]))
                        {
                            //skip
                        }

                        else
                        {
                            tempDay.Add(inputDay[i]);
                            tempDate.Add(inputDate[i]);
                            tempCourse.Add(inputCourse[i]);
                            tempCode.Add(inputCode[i]);
                            tempSurface.Add(inputSurface[i]);
                            tempSession.Add(inputSession[i]);
                            tempType.Add(inputType[i]);
                            tempNumberOfLines++;
                        }
                    }

                    inputDay = new List<string>();
                    inputDate = new List<string>();
                    inputCourse = new List<string>();
                    inputCode = new List<string>();
                    inputSurface = new List<string>();
                    inputSession = new List<string>();
                    inputType = new List<string>();
                    numberOfLines = tempNumberOfLines;

                    inputDay = tempDay;
                    inputDate = tempDate;
                    inputCourse = tempCourse;
                    inputCode = tempCode;
                    inputSurface = tempSurface;
                    inputSession = tempSession;
                    inputType = tempType;
                }

                writeLine("");
                writeLine("Ready to write data to CSV. Do you wish to continue?");
                writeLine("Y/N");

                userResponseKey = Console.ReadKey();
                userResponse = userResponseKey.KeyChar.ToString();
                Console.Write("\b");

                if (userResponse == "Y" || userResponse == "y")
                {
                    writeLine("");
                    writeLine("Reformatting course names for RacecourseGuide.co.uk");

                    #region Reformat Course Names
                    for (int i = 0; i < numberOfLines; i++)
                    {
                        switch(inputCourse[i])
                        {
                            case "Bangor-On-Dee":
                                inputCourse[i] = "Bangor";
                                break;
                            case "Catterick Bridge":
                                inputCourse[i] = "Catterick";
                                break;
                            case "Chelmsford City":
                                inputCourse[i] = "Chelmsford";
                                break;
                            case "Epsom Downs":
                                inputCourse[i] = "epsom";
                                break;
                            case "Ffos Las":
                                inputCourse[i] = "ffos-las";
                                break;
                            case "Fontwell Park":
                                inputCourse[i] = "Fontwell";
                                break;
                            case "Hamilton Park":
                                inputCourse[i] = "Hamilton";
                                break;
                            case "Haydock Park":
                                inputCourse[i] = "Haydock";
                                break;
                            case "Kempton Park":
                                inputCourse[i] = "Kempton";
                                break;
                            case "Lingfield Park":
                                inputCourse[i] = "Lingfield";
                                break;
                            case "Market Rasen":
                                inputCourse[i] = "Market-Rasen";
                                break;
                            case "Newton Abbot":
                                inputCourse[i] = "newton-abbot";
                                break;
                            case "Ripon":
                                inputCourse[i] = "ripon";
                                break;
                            case "Sandown Park":
                                inputCourse[i] = "Sandown";
                                break;
                            case "Stratford-On-Avon":
                                inputCourse[i] = "Stratford";
                                break;
                            case "Yarmouth":
                                inputCourse[i] = "great-yarmouth";
                                break;
                        }
                    }
                    #endregion

                    writeLine("Creating alternate date structure");
                    List<string> alternateDate = new List<string>();
                    List<string> dateDay = new List<string>();
                    List<string> dateMonth = new List<string>();
                    List<string> dateYear = new List<string>();
                    for (int i = 0; i < numberOfLines; i++)
                    {
                        string[] dateStructure = inputDate[i].Split('/');

                        dateDay.Add(dateStructure[0]);
                        dateMonth.Add(dateStructure[1]);
                        dateYear.Add(dateStructure[2]);

                        alternateDate.Add(dateStructure[2] + "-" + dateStructure[1] + "-" + dateStructure[0]);

                    }

                    writeLine("Creating post title");
                    List<string> postTitles = new List<string>();
                    for (int i = 0; i < numberOfLines; i++)
                    {

                        switch (dateMonth[i])
                        {
                            case "01":
                                dateMonth[i] = "January";
                                break;
                            case "02":
                                dateMonth[i] = "February";
                                break;
                            case "03":
                                dateMonth[i] = "March";
                                break;
                            case "04":
                                dateMonth[i] = "April";
                                break;
                            case "05":
                                dateMonth[i] = "May";
                                break;
                            case "06":
                                dateMonth[i] = "June";
                                break;
                            case "07":
                                dateMonth[i] = "July";
                                break;
                            case "08":
                                dateMonth[i] = "August";
                                break;
                            case "09":
                                dateMonth[i] = "September";
                                break;
                            case "10":
                                dateMonth[i] = "October";
                                break;
                            case "11":
                                dateMonth[i] = "November";
                                break;
                            case "12":
                                dateMonth[i] = "December";
                                break;
                        }


                        postTitles.Add(inputCourse[i] + " (" + inputSession[i] + " " + inputCode[i] + " Meeting) - " + inputDay[i] + " " + dateDay[i] + " " + dateMonth[i] + " " + dateYear[i]);

                    }


                    writeLine("Building output content");
                    List<string> output = new List<string>();

                    Random rnd = new Random();
                    for (int i = 0; i < numberOfLines; i++)
                    {
                        int randomised = rnd.Next(0, 19);
                        string featuredImage = "";
                        if (inputCode[i] == "Jump")
                        {
                            switch (randomised)
                            {
                                case 0:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2017/03/World-Hurdle-2013-Solwhit-2.jpg";
                                    break;
                                case 1:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2017/03/Packed-Stand-At-Cheltenham.jpg";
                                    break;
                                case 2:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2017/03/Champion-Chase-SprinterSacre2.jpg";
                                    break;
                                case 3:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2017/01/Leicester-Racecourse-Chase-Fence.jpg";
                                    break;
                                case 4:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2017/01/Kelso-Racecourse-Hurdles.jpg";
                                    break;
                                case 5:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/12/Chase-Fence-At-Kempton-Park-Racecourse.jpg";
                                    break;
                                case 6:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/12/Albert-Bartlett-Novice-Hurdle-At-Fishers-Cross.jpg";
                                    break;
                                case 7:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/12/WAR551-7940-Race-5-Winner-The-New-One-12-01-13.jpg";
                                    break;
                                case 8:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/11/IMG_3590.jpg.jpg";
                                    break;
                                case 9:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/11/Cheltenham-Racecourse-World-Hurdle-2013-Solwhit.jpg";
                                    break;
                                case 10:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/11/cheltenham-46.jpg";
                                    break;
                                case 11:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/11/Horse_Racing_Kempton_250212_TGS036.jpg";
                                    break;
                                case 12:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/11/Ryanair-Chase-2013CueCard-Ch12.jpg";
                                    break;
                                case 13:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/10/Doncasterhurdles2.jpg";
                                    break;
                                case 14:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/10/035.jpg";
                                    break;
                                case 15:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/09/warwick-28.jpg";
                                    break;
                                case 16:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/09/warwick-13.jpg";
                                    break;
                                case 17:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/09/market-rasen-81.jpg";
                                    break;
                                case 18:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/09/market-rasen-51.jpg";
                                    break;
                                case 19:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/09/market-rasen-39.jpg";
                                    break;


                            }
                        }

                        else if(inputCode[i] == "Flat")
                        {
                            switch(randomised)
                            {
                                case 0:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2017/03/chelmsford-27.jpg";
                                    break;
                                case 1:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/10/L28-029.jpg";
                                    break;
                                case 2:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/09/york-78.jpg";
                                    break;
                                case 3:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/09/york-77.jpg";
                                    break;
                                case 4:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/09/york-76.jpg";
                                    break;
                                case 5:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/09/york-75.jpg";
                                    break;
                                case 6:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/09/york-68.jpg";
                                    break;
                                case 7:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/09/york-63.jpg";
                                    break;
                                case 8:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/09/york-21.jpg";
                                    break;
                                case 9:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/09/wolverhampton-70.jpg";
                                    break;
                                case 10:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/09/wolverhampton-65.jpg";
                                    break;
                                case 11:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/09/wolverhampton-50.jpg";
                                    break;
                                case 12:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/09/wolverhampton-36.jpg";
                                    break;
                                case 13:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/09/wolverhampton-20.jpg";
                                    break;
                                case 14:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/09/wolverhampton-7.jpg";
                                    break;
                                case 15:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/09/warwick-5.jpg";
                                    break;
                                case 16:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/09/southwell-aw-20.jpg";
                                    break;
                                case 17:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/09/pontefract-55.jpg";
                                    break;
                                case 18:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/09/pontefract-29.jpg";
                                    break;
                                case 19:
                                    featuredImage = "http://racecourseguide.co.uk/wp-content/uploads/2016/09/lingfield-flat-6.jpg";
                                    break;
                            }

                        }


                        output.Add(postTitles[i] + "," + "RPG" + "," + "01/01/1970" + "," + "publish" + "," + featuredImage + "," + alternateDate[i] + "," + (inputCourse[i] + " " + inputDate[i]) + "," + inputCourse[i]);

                    }

                    writeLine("Writing content to CSV");
                    string outputPath = Path.Combine(desktopPath, "RPGFixtures.csv");

                    using (StreamWriter outputFile = new StreamWriter(outputPath))
                    {
                        for(int i = 0; i < numberOfLines; i++)
                        {
                            outputFile.WriteLine(output[i]);
                        }

                        outputFile.Close();

                    }

                    writeLine("");
                    writeLine("Program has finished executing.");

                    writeLine("");
                    writeLine("Instructions for importing into Wordpress:");
                    writeLine("(1) - Sign into racecourseguide.co.uk/wp-admin using the RPG account                                            ");
                    writeLine("(2) - Mouse over 'Ultimate CSV Importer Free' in the sidebar and then click on Import/Update                    ");
                    writeLine("(3) - Make sure you're on the Upload from Desktop option and click the upload button in the middle of the window");
                    writeLine("(4) - Navigate to the desktop in the file manager window and upload 'RPGFixtures'                               ");
                    writeLine("(5) - Select 'New Items' and Choose to Import each record as 'race-day' before hitting Continue                 ");
                    writeLine("(6) - Alter the CSV Header dropdowns to match the following...                                                  ");
                    writeLine("      Title - 'i.e. Chelmsford 01/01/2017'                                                                      ");
                    writeLine("      Publish Date - '01/01/1970'                                                                               ");
                    writeLine("      Author - 'RPG'                                                                                            ");
                    writeLine("      Status - 'publish'                                                                                        ");
                    writeLine("      Featured Image - 'i.e. http://racecourseguide....'                                                        ");
                    writeLine("      race_day_date - 'i.e. 2017-12-01'                                                                         ");
                    writeLine("      race_day_title - 'i.e. Chelmsford (Evening Flat...)                                                       ");
                    writeLine("      Racecourse - 'i.e. Chelmsford'                                                                            ");
                    writeLine("(7) - Press continue on the following two pages and then click on 'Schedule Now'                                ");
                    writeLine("(8) - Wait for it to upload                                                                                     ");
                    writeLine("(9) - Profit                                                                                                    ");

                }

                else
                {
                    writeLine("");
                    writeLine("Operation cancelled. You are free to close this window.");
                }

                Console.ReadKey();
            }
        }
    }
}
