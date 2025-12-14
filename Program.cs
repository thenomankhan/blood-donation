using System;
//1st part//
class Person
{
    public string name;
    public string phone;
    private string password;
    public string Password
    {
        get { return password; }
        set { password = value; }
    }
    public Person(string name, string phone, string password)
    {
        this.name = name;
        this.phone = phone;
        this.password = password;
    }
}

// ---------------- Inharitance -----------------
class Doner : Person
{
    public string division;
    public string district;
    public string thana;
    public string bloodGroup;
    public string lastDonationDate;
    public Doner(string name, string phone, string password) : base(name, phone, password)
    {
        this.division = "null";
        this.district = "null";
        this.thana = "null";
        this.bloodGroup = "null";
        this.lastDonationDate = "null";
    }
    public Doner(string name, string phone, string password, string division, string district, string thana, string bloodGroup) : base(name, phone, password)
    {
        this.division = division;
        this.district = district;
        this.thana = thana;
        this.bloodGroup = bloodGroup;
        this.lastDonationDate = "null";
    }
}

class Consumer : Person
{
    public Consumer(string name, string phone, string password) : base(name, phone, password) { }
}

// ------------------ abstraction -------------------
abstract class Request
{
    public int bloodRequestId;
    public string consumerName;
    public string consumerPhone;
    public string consumerDivision;
    public string consumerDistrict;
    public string consumerThana;
    public string consumerHospital;
    public string consumerBloodGroup;
    public int quantity;
    public string dateTime;
    public bool accepted;
    public string acceptedDonerName;
    public string acceptedDonerAddress;
    public string acceptedDonerPhone;

}
class BloodRequest : Request
{
    public BloodRequest(int bloodRequestId, string consumerName, string consumerPhone, string division, string district, string thana, string hospital, string bloodGroup, int quantity, string date, string time)
    {
        this.bloodRequestId = bloodRequestId;
        this.consumerName = consumerName;
        this.consumerPhone = consumerPhone;
        this.consumerDivision = division;
        this.consumerDistrict = district;
        this.consumerThana = thana;
        this.consumerHospital = hospital;
        this.consumerBloodGroup = bloodGroup;
        this.quantity = quantity;
        this.accepted = false;
        this.dateTime = $"{date} {time}";

       
    }
}
//2nd part//
static class ManageBloodRequest
{
    
    public static BloodRequest[] bloodRequests = new BloodRequest[1000];
    public static int bloodRequestIndex = 0;
    public static void AddRequest(string consumerName, string consumerPhone, string division, string district, string thana, string hospital, string bloodGroup, int quantity, string date, string time)
    {
        bloodRequests[bloodRequestIndex] = new BloodRequest(bloodRequestIndex + 1, consumerName, consumerPhone, division, district, thana, hospital, bloodGroup, quantity, date, time);
        bloodRequestIndex++;
    }
    public static void Accept(Doner doner, int id)
    {
        bloodRequests[id - 1].accepted = true;
        bloodRequests[id - 1].acceptedDonerName = doner.name;
        bloodRequests[id - 1].acceptedDonerAddress = $"{doner.division}, {doner.district}, {doner.thana}";
        bloodRequests[id - 1].acceptedDonerPhone = doner.phone;

        doner.lastDonationDate = bloodRequests[id - 1].dateTime;
        Console.WriteLine("Accepted request id: " + bloodRequests[id - 1].bloodRequestId);
    }
    public static void ShowConsumerRequest(string phone)
    {
        bool isRequestExists = false;
        Console.WriteLine("-----------------------------------------------------------------");
        for (int i = bloodRequestIndex - 1; i >= 0; i--)
        {
            BloodRequest bloodRequest = bloodRequests[i];

            if (bloodRequest.consumerPhone == phone)
            {
                isRequestExists = true;
                Console.WriteLine("Blood Group: " + bloodRequest.consumerBloodGroup);
                Console.WriteLine($"Quantity: {bloodRequest.quantity}");
                Console.WriteLine($"Date & Time: {bloodRequest.dateTime}");
                Console.WriteLine($"Address: {bloodRequest.consumerDivision}, {bloodRequest.consumerDistrict}, {bloodRequest.consumerThana}, {bloodRequest.consumerHospital}");
                if (bloodRequest.accepted)
                {
                    Console.WriteLine("Accepted by:");
                    Console.WriteLine($"Name: {bloodRequest.acceptedDonerName}");
                    Console.WriteLine("Phone: " + bloodRequest.acceptedDonerPhone);
                    Console.WriteLine($"Address: {bloodRequest.acceptedDonerAddress}");
                }
                Console.WriteLine("-----------------------------------------------------------------");
            }
        }
        if (!isRequestExists)
        {
            Console.WriteLine("You have zero request!");
        }
    }
    public static void ShowDonerRequest(Doner doner)
    {
        bool isRequestExists = false;
        Console.WriteLine("-----------------------------------------------------------------");
        for (int i = bloodRequestIndex - 1; i >= 0; i--)
        {
            BloodRequest bloodRequest = bloodRequests[i];
            if (bloodRequest.consumerDistrict.ToLower() == doner.district.ToLower() && bloodRequest.consumerDivision.ToLower() == doner.division.ToLower() && bloodRequest.consumerThana.ToLower() == doner.thana.ToLower() && bloodRequest.consumerBloodGroup.ToLower() == doner.bloodGroup.ToLower() && bloodRequest.accepted == false)
            {
                isRequestExists = true;
                Console.WriteLine("Request id: " + bloodRequest.bloodRequestId);
                Console.WriteLine("Consumer name: " + bloodRequest.consumerName);
                Console.WriteLine("Blood Group: " + bloodRequest.consumerBloodGroup);
                Console.WriteLine($"Quantity: {bloodRequest.quantity}");
                Console.WriteLine($"Date & Time: {bloodRequest.dateTime}");
                Console.WriteLine($"Address: {bloodRequest.consumerDivision}, {bloodRequest.consumerDistrict}, {bloodRequest.consumerThana}, {bloodRequest.consumerHospital}");
                Console.WriteLine("-----------------------------------------------------------------");
            }
        }
        if (!isRequestExists)
        {
            Console.WriteLine("No request available now");
        }
    }
    public static void ShowDonerAcceptedRequest(Doner doner)
    {
        bool isRequestExists = false;
        Console.WriteLine("-----------------------------------------------------------------");
        for (int i = 0; i < bloodRequestIndex; i++)
        {
            BloodRequest bloodRequest = bloodRequests[i];
            if (bloodRequests[i].accepted == true && bloodRequests[i].acceptedDonerPhone == doner.phone)
            {
                isRequestExists = true;
                Console.WriteLine("Request id: " + bloodRequest.bloodRequestId);
                Console.WriteLine("Consumer name: " + bloodRequest.consumerName);
                Console.WriteLine("Blood Group: " + bloodRequest.consumerBloodGroup);
                Console.WriteLine($"Quantity: {bloodRequest.quantity}");
                Console.WriteLine($"Date & Time: {bloodRequest.dateTime}");
                Console.WriteLine($"Consumer Phone: " + bloodRequest.consumerPhone);
                Console.WriteLine($"Address: {bloodRequest.consumerDivision}, {bloodRequest.consumerDistrict}, {bloodRequest.consumerThana}, {bloodRequest.consumerHospital}");
                Console.WriteLine("-----------------------------------------------------------------");
            }
        }
        if (!isRequestExists)
        {
            Console.WriteLine("No accepted request available right now");
        }
    }
}
//3rd part//
static class ManageUser
{
    private static Doner[] doners = new Doner[100];
    private static int donerIndex = 0;

    private static Consumer[] consumers = new Consumer[100];
    private static int consumerIndex = 0;


    // ----------- Doner management --------------

    public static void EditDonerDetails(string phone, string division, string district, string thana, string bloodGroup)
    {
        for (int i = 0; i < donerIndex; i++)
        {
            if (doners[i].phone == phone)
            {
                doners[i].division = division;
                doners[i].district = district;
                doners[i].thana = thana;
                doners[i].bloodGroup = bloodGroup;
            }
        }
    }

    public static void AddDoner(string name, string phone, string password)
    {
        doners[donerIndex] = new Doner(name, phone, password);
        donerIndex++;
    }
    public static void AddDoner(string name, string phone, string password, string division, string district, string thana, string bloodGroup)
    {
        doners[donerIndex] = new Doner(name, phone, password, division, district, thana, bloodGroup);
        donerIndex++;
    }

    // checking phone number is already registered or not method
    public static bool DonerPhnAvailable(string phone)
    {
        for (int i = 0; i < donerIndex; i++)
        {
            if (doners[i].phone == phone)
            {
                return false;
            }
        }
        return true;
    }

    public static bool DnrCradintialsCorrect(string phone, string password)
    {
        for (int i = 0; i < donerIndex; i++)
        {
            if (doners[i].phone == phone && doners[i].Password == password)
            {
                return true;
            }
        }
        return false;
    }

    public static Doner GetDoner(string phone)
    {
        for (int i = 0; i < donerIndex; i++)
        {
            if (doners[i].phone == phone)
            {
                return doners[i];
            }
        }
        return new Doner("null", "null", "null");
    }

    public static bool ShowPeople(string division, string district, string thana, string bloodGroup)
    {
        Console.WriteLine("-------------------------------------------------------");
        bool isPeopleExists = false;
        for (int i = 0; i < donerIndex; i++)
        {
            Doner doner = doners[i];
            if (doner.division.ToLower() == division.ToLower() && doner.district.ToLower() == district.ToLower() && doner.thana.ToLower() == thana.ToLower() && doner.bloodGroup.ToLower() == bloodGroup.ToLower())
            {
                isPeopleExists = true;

                Console.WriteLine($"Name: {doner.name}");
                Console.WriteLine("Blood group: " + doner.bloodGroup);
                Console.WriteLine($"Address: {doner.division}, {doner.district}, {doner.thana}");
                Console.WriteLine("-------------------------------------------------------");
            }
        }
        if (!isPeopleExists)
        {
            Console.WriteLine("No one found..");
            return false;
        }
        else
        {
            return true;
        }
    }



    // ----------- Consumer management -------------
    public static void AddConsumer(string name, string phone, string password)
    {
        consumers[consumerIndex] = new Consumer(name, phone, password);
        consumerIndex++;
    }
    public static bool ConsumerPhnAvailable(string phone)
    {
        for (int i = 0; i < consumerIndex; i++)
        {
            if (consumers[i].phone == phone)
            {
                return false;
            }
        }
        return true;
    }

    public static bool ConsCradintialsCorrect(string phone, string password)
    {
        for (int i = 0; i < consumerIndex; i++)
        {
            if (consumers[i].phone == phone && consumers[i].Password == password)
            {
                return true;
            }
        }
        return false;
    }
    public static Consumer GetConsumer(string phone)
    {
        for (int i = 0; i < consumerIndex; i++)
        {
            if (consumers[i].phone == phone)
            {
                return consumers[i];
            }
        }
        return new Consumer("null", "null", "null");
    }

}

//4th and 5th part//
class Program
{
    public static Doner loggedInDoner;
    public static Consumer loggedInConsumer;

    // --------------- Authentication ---------------
    public static void Authentication()
    {
        Console.WriteLine("--------- Authentication ------------");
        Console.WriteLine("1. Login as Doner");
        Console.WriteLine("2. Login as Consumer");
        Console.WriteLine("3. Registration");

        Console.Write("Choose: ");

        try
        {
            int input = Convert.ToInt32(Console.ReadLine());

            if (input == 1)
            {
                LoginDoner();
            }
            else if (input == 2)
            {
                LoginConsumer();
            }
            else if (input == 3)
            {
                Registration();
            }
            else
            {
                Console.WriteLine("Invalid input!!");
                Authentication();
            }


        }
        catch (Exception)
        {
            Console.WriteLine("Something went wrong!");
            Console.WriteLine("Login again your data is saved");
            Authentication();
        }
    }
    public static void Registration()
    {
        Console.WriteLine("----------- Registration ------------");
        Console.WriteLine("1. As Doner");
        Console.WriteLine("2. As Consumer");
        Console.Write("Choose: ");
        try
        {
            int input = Convert.ToInt32(Console.ReadLine());

            if (input == 1)
            {
                DonerRegistration();
            }
            else if (input == 2)
            {
                ConsumerRegistration();
            }
            else
            {
                Console.WriteLine("Invalid input!");
                Registration();
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Something went wrong!!");
            Registration();
        }
    }
    public static void DonerRegistration()
    {
        Console.WriteLine("-------- Doner Registration ----------");
        Console.Write("Enter phone number: ");
        string phone = Console.ReadLine();
        if (!ManageUser.DonerPhnAvailable(phone))
        {
            Console.WriteLine("Phone number already used!");
            DonerRegistration();
        }

        Console.Write("Enter name: ");
        string name = Console.ReadLine();

        Console.Write("Create password: ");
        string password = Console.ReadLine();

        Console.WriteLine("1. Add address now");
        Console.WriteLine("2. Create account anyway");
        Console.Write("Choose: ");
        int input = Convert.ToInt32(Console.ReadLine());

        if (input == 1)
        {
            ManageUser.AddDoner(name, phone, password);
            EditDonerDetails(phone);
        }
        else
        {
            ManageUser.AddDoner(name, phone, password);

        }
        Console.WriteLine("Registration successfull!");
        Authentication();
    }
    public static void ConsumerRegistration()
    {
        Console.WriteLine("--------- Consumer Registration ------------");
        Console.Write("Enter phone: ");
        string phone = Console.ReadLine();
        if (!ManageUser.ConsumerPhnAvailable(phone))
        {
            Console.WriteLine("Phone number already used!");
            ConsumerRegistration();
        }
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();
        Console.Write("Create password: ");
        string password = Console.ReadLine();

        ManageUser.AddConsumer(name, phone, password);
        Console.WriteLine("Registration successfull!");
        Authentication();
    }

    // ----------------- login ------------------
    public static void LoginDoner()
    {
        Console.WriteLine("---------- Login -------------");
        Console.Write("Enter your phone number: ");
        string phone = Console.ReadLine();
        Console.Write("Enter your password: ");
        string password = Console.ReadLine();

        if (ManageUser.DnrCradintialsCorrect(phone, password))
        {

            // home
            Console.WriteLine("Welcome Home");
            loggedInDoner = ManageUser.GetDoner(phone);
            DonerHome();
        }
        else
        {
            Console.WriteLine("Phone or password is incorrect!");
            Authentication();
        }

    }
    public static void LoginConsumer()
    {
        Console.WriteLine("------------ Login Consumer -----------");
        Console.Write("Enter your phone number: ");
        string phone = Console.ReadLine();
        Console.Write("Enter your password: ");
        string password = Console.ReadLine();

        if (ManageUser.ConsCradintialsCorrect(phone, password))
        {
            loggedInConsumer = ManageUser.GetConsumer(phone);
            ConsumerHome();
        }
        else
        {
            Console.WriteLine("Phone or password is incorrect!");
            Authentication();
        }
    }

    // ------------------ doner home section ------------------
    public static void DonerHome()
    {
        Console.WriteLine("---------- Home ----------");
        Console.WriteLine("Welcome " + loggedInDoner.name);
        Console.WriteLine("1. My profile");
        Console.WriteLine("2. Blood request");
        Console.WriteLine("3. Accepted Request");
        Console.WriteLine("0. Logout");

        Console.Write("Choose: ");
        int input = Convert.ToInt32(Console.ReadLine());

        if (input == 1)
        {
            DonerProfile();
        }
        else if (input == 2)
        {
            DonerBloodRequest();
        }
        else if (input == 3)
        {
            donerAcceptedrequest();
        }
        else
        {
            Authentication();
        }
    }
    public static void EditDonerDetails(string phone)
    {
        Console.Write("Enter your division: ");
        string division = Console.ReadLine();
        Console.Write("Enter your district: ");
        string district = Console.ReadLine();
        Console.Write("Enter your Thana: ");
        string thana = Console.ReadLine();
        Console.Write("Enter your blood group: ");
        string bloodGroup = Console.ReadLine();

        ManageUser.EditDonerDetails(phone, division, district, thana, bloodGroup);
    }
    public static void DonerProfile()
    {
        Console.WriteLine("------------- Profile -----------");
        Console.WriteLine("Name: " + loggedInDoner.name);
        Console.WriteLine("Pone: " + loggedInDoner.phone);
        Console.WriteLine("Division: " + loggedInDoner.division);
        Console.WriteLine("District: " + loggedInDoner.district);
        Console.WriteLine("Thana: " + loggedInDoner.thana);
        Console.WriteLine("Blood group: " + loggedInDoner.bloodGroup);
        Console.WriteLine("Last blood donation date: " + loggedInDoner.lastDonationDate);

        Console.WriteLine("1. Edit profile");
        Console.WriteLine("0. Back to home");

        Console.Write("Choose: ");
        int input = Convert.ToInt32(Console.ReadLine());
        if (input == 1)
        {
            EditDonerDetails(loggedInDoner.phone);
            Console.WriteLine("Edit successfull");
            DonerProfile();
        }
        else
        {
            DonerHome();
        }
    }
    public static void DonerBloodRequest()
    {
        Console.WriteLine("----------- Blood Request -------------");
        ManageBloodRequest.ShowDonerRequest(loggedInDoner);

        Console.WriteLine("1. Accept any");
        Console.WriteLine("0. Back to home");
        Console.Write("Choose: ");
        string input = Console.ReadLine();
        if (input == "1")
        {
            Console.Write("Enter request id: ");
            int requestId = Convert.ToInt32(Console.ReadLine());

            ManageBloodRequest.Accept(loggedInDoner, requestId);

            DonerBloodRequest();
        }
        else
        {
            DonerHome();
        }

    }
    public static void donerAcceptedrequest()
    {
        ManageBloodRequest.ShowDonerAcceptedRequest(loggedInDoner);

        Console.WriteLine("0. Back to home");
        Console.Write("Choose: ");
        string input = Console.ReadLine();

        DonerHome();

    }

    // ----------------- consumer home section ------------------
    public static void ConsumerHome()
    {
        Console.WriteLine("------------ Home -------------");
        Console.WriteLine("Welcome " + loggedInConsumer.name);
        Console.WriteLine("1. Profile");
        Console.WriteLine("2. Send Request");
        Console.WriteLine("3. My Request");
        Console.WriteLine("0. Logout");

        Console.Write("Choose: ");
        int input = Convert.ToInt32(Console.ReadLine());

        if (input == 1)
        {
            ConsumerProfile();
        }
        else if (input == 2)
        {
            SendBloodRequest();
        }
        else if (input == 3)
        {
            ShowConsumerRequest();
        }
        else
        {
            Authentication();
        }
    }
    public static void ConsumerProfile()
    {
        Console.WriteLine("------------ Profile -------------");
        Console.WriteLine("Name: " + loggedInConsumer.name);
        Console.WriteLine("Phone: " + loggedInConsumer.phone);

        Console.WriteLine("0. Back to home");
        Console.Write("Choose: ");
        string input = Console.ReadLine();
        ConsumerHome();
    }
    public static void SendBloodRequest()
    {
        Console.WriteLine("-------------- Send Request ------------");
        Console.Write("Enter division: ");
        string division = Console.ReadLine();

        Console.Write("Enter district: ");
        string district = Console.ReadLine();

        Console.Write("Enter Thana: ");
        string thana = Console.ReadLine();

        Console.Write("Enter hospital: ");
        string hospital = Console.ReadLine();

        Console.Write("Enter blood group: ");
        string bloodGroup = Console.ReadLine();

        Console.Write("Blood quantity: ");
        int quantity = Convert.ToInt32(Console.ReadLine());

        Console.Write("Date (MM-DD-YYYY): ");
        string date = Console.ReadLine();

        Console.Write("Time (24HH:MM): ");
        string time = Console.ReadLine();

        Console.Write("Search people?(Y/n): ");
        string input = Console.ReadLine().ToLower();

        if (input != "n")
        {
            bool isPeopleFound = ManageUser.ShowPeople(division, district, thana, bloodGroup);
            if (isPeopleFound)
            {
                Console.Write("Send request to all?(Y/n)");
                string choice = Console.ReadLine().ToLower();
                if (choice != "n")
                {
                    ManageBloodRequest.AddRequest(loggedInConsumer.name, loggedInConsumer.phone, division, district, thana, hospital, bloodGroup, quantity, date, time);
                    Console.WriteLine("Request send successfully");
                }

            }
            else
            {
                Console.Write("Send request anyway(Y/n): ");
                string choice = Console.ReadLine().ToLower();
                if (choice != "n")
                {
                    ManageBloodRequest.AddRequest(loggedInConsumer.name, loggedInConsumer.phone, division, district, thana, hospital, bloodGroup, quantity, date, time);
                    Console.WriteLine("Request send successfully");
                }
            }
        }

        ConsumerHome();

    }
    public static void ShowConsumerRequest()
    {
        Console.WriteLine("-------------- My request -------------");
        ManageBloodRequest.ShowConsumerRequest(loggedInConsumer.phone);
        Console.WriteLine("0. Back to home");
        Console.Write("Choose: ");
        string input = Console.ReadLine();
        ConsumerHome();
    }

    public static void Main()
    {
        Authentication();
    }
}
