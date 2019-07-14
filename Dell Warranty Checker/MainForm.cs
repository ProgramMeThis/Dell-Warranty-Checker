using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Security;
using Newtonsoft.Json;
using System.Net;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using System.Data.SqlClient;
using System.Net.NetworkInformation;

namespace Dell_Warranty_Checker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private TextBox lastTextBoxChanged = null;

        ErrorProvider ep1 = new ErrorProvider();

        string apiKey = "API Key Goes Here";

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lookupInformation()
        {
            lookupButton.Enabled = false;
            lookupButton.Text = "Looking Up...";
            resetButton.Enabled = false;
            resultsTextBox.Text = "";

            if (lastTextBoxChanged == computerNameTextBox)
            {
                lookUpByHostName(computerNameTextBox.Text);
            }
            else if (lastTextBoxChanged == serviceTagTextBox)
            {
                lookupByServiceTag(serviceTagTextBox.Text);
            }
            else if (lastTextBoxChanged == ipAddressTextBox)
            {
                lookupByIP(ipAddressTextBox.Text);
            }
            else
            {
                //come up with some message
            }

            lookupButton.Enabled = true;
            lookupButton.Text = "Lookup";
            resetButton.Enabled = true;
        }

        private void lookupButton_Click(object sender, EventArgs e)
        {
            lookupInformation();
        }

        private void getWarrantyInfoFromServiceTag(string serviceTagNumber)
        {
            if (serviceTagNumber != "")
            {
                string jsonURL = "https://api.dell.com/support/assetinfo/v4/getassetwarranty/" + serviceTagNumber + "?apikey=" + apiKey;

                using (var w = new WebClient())
                {
                    try
                    {
                        var json_data = string.Empty;
                        DateTime startTime = DateTime.Now;
                        json_data = w.DownloadString(jsonURL);
                        DateTime endTime = DateTime.Now;

                        Rootobject ro = JsonConvert.DeserializeObject<Rootobject>(json_data);

                        if (ro != null && ro.AssetWarrantyResponse != null)
                        {
                            string machineDescription = ro.AssetWarrantyResponse[0].AssetHeaderData.MachineDescription;
                            resultsTextBox.Text += "Machine Description: " + machineDescription + Environment.NewLine;

                            foreach (Assetentitlementdata warrantyInfo in ro.AssetWarrantyResponse[0].AssetEntitlementData)
                            {
                                DateTime endDate = warrantyInfo.EndDate;
                                DateTime startDate = warrantyInfo.StartDate;
                                string serviceLevelCode = warrantyInfo.ServiceLevelCode;
                                string serviceLevelDescription = warrantyInfo.ServiceLevelDescription;
                                string entitlementType = warrantyInfo.EntitlementType;

                                resultsTextBox.Text += serviceLevelCode + " - " + serviceLevelDescription + " (" + startDate.ToString("MM/dd/yyyy") + " - " + endDate.ToString("MM/dd/yyyy") + ")" + Environment.NewLine;
                            }
                        }
                        else
                        {
                            TimeSpan lookupTime = endTime - startTime;
                            if (lookupTime.Seconds > 5)
                            {
                                MessageBox.Show("The lookup timed out while trying to pull the warranty information from Dell. Please wait a minute and try again.", "Service Tag Lookup Timed Out");
                            }
                            else
                            {
                                MessageBox.Show("The service tag did not match anything in Dell's records. Please ensure the service tag is entered correctly.", "Invalid Service Tag");
                            }
                            resultsTextBox.Text = "";
                        }
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("An error occurred getting the warranty data. Error: " + exc.Message);
                    }
                }
            }
        }

        private void getHostNameFromIPAddress(string ipAddress)
        {
            try
            {
                computerNameTextBox.Text = Dns.GetHostEntry(@ipAddress).HostName;
            }
            catch (Exception)
            {
                MessageBox.Show("A host name could not be resolved from the IP Address. Please ensure the IP Address is entered correctly and that the machine is currently online.", "Unable to Resolve Host Name from IP Address");
            }
        }

        private void GetIPAddressFromHostName(string hostName)
        {
            if (hostName != null && hostName != "")
            {
                try
                {
                    IPHostEntry ipEntry = Dns.GetHostEntry(hostName);
                    IPAddress[] addr = ipEntry.AddressList;

                    string ipAddress = "";
                    for (int i = 0; i < addr.Length; i++)
                    {
                        if (addr[i].AddressFamily != System.Net.Sockets.AddressFamily.InterNetworkV6)
                        {
                            ipAddress = addr[i].ToString();
                        }
                    }

                    ipAddressTextBox.Text = ipAddress;
                }
                catch (System.Net.Sockets.SocketException)
                {
                    //do nothing because it means we couldn't resolve an IP for the DNS name
                }
            }
        }

        internal static string getServiceTagFromHostName(string hostName, bool showConnectionError)
        {
            if (hostName != null && hostName != "")
            {
                ConnectionOptions options = new ConnectionOptions();
                options.Impersonation = System.Management.ImpersonationLevel.Impersonate;

                ManagementScope scope = new ManagementScope("\\\\" + hostName + "\\root\\cimv2");
                try
                {
                    scope.Connect();

                    //Query system for Operating System information
                    ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_BIOS");
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

                    ManagementObjectCollection queryCollection = searcher.Get();
                    foreach (ManagementObject m in queryCollection)
                    {
                        return m["serialnumber"].ToString();
                        //serviceTagTextBox.Text = m["serialnumber"].ToString();
                    }
                }
                catch (Exception exc)
                {
                    if (showConnectionError)
                    {
                        MessageBox.Show(hostName + " was unable to be connected to. Please ensure the hostname is entered correctly and that the device is online.", "Unable to Connect to " + hostName);
                    }
                }
            }

            return "";
        }

        private void lookupByIP(string ipAddress)
        {
            computerNameTextBox.Text = "";
            serviceTagTextBox.Text = "";
            getHostNameFromIPAddress(ipAddress);
            serviceTagTextBox.Text = getServiceTagFromHostName(computerNameTextBox.Text, true);
            getWarrantyInfoFromServiceTag(serviceTagTextBox.Text);
        }

        private void lookupByServiceTag(string serviceTag)
        {
            computerNameTextBox.Text = "";
            ipAddressTextBox.Text = "";
            getWarrantyInfoFromServiceTag(serviceTag);
        }

        private void lookUpByHostName(string hostName)
        {
            serviceTagTextBox.Text = "";
            ipAddressTextBox.Text = "";
            serviceTagTextBox.Text = getServiceTagFromHostName(hostName, true);
            GetIPAddressFromHostName(hostName);
            getWarrantyInfoFromServiceTag(serviceTagTextBox.Text);
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            resetScreen();
        }

        private void resetScreen()
        {
            resultsTextBox.Text = "";
            serviceTagTextBox.Text = "";
            computerNameTextBox.Text = "";
            ipAddressTextBox.Text = "";
            lastTextBoxChanged = null;
        }

        private void computerNameTextBox_TextChanged(object sender, EventArgs e)
        {
            textBoxesChanged();
            lastTextBoxChanged = computerNameTextBox;
        }

        private void serviceTagTextBox_TextChanged(object sender, EventArgs e)
        {
            textBoxesChanged();
            lastTextBoxChanged = serviceTagTextBox;
        }

        private void ipAddressTextBox_TextChanged(object sender, EventArgs e)
        {
            textBoxesChanged();
            lastTextBoxChanged = ipAddressTextBox;
        }

        private void resultsTextBox_TextChanged(object sender, EventArgs e)
        {
            textBoxesChanged();
        }

        private void textBoxesChanged()
        {
            if (lookupButton.Text == "Lookup")
            {
                if (computerNameTextBox.Text.Trim() != "" || serviceTagTextBox.Text.Trim() != "" || ipAddressTextBox.Text.Trim() != "")
                {
                    lookupButton.Enabled = true;
                    resetButton.Enabled = true;
                }
                else
                {
                    lookupButton.Enabled = false;
                    if (resultsTextBox.Text.Trim() != "")
                    {
                        resetButton.Enabled = true;
                    }
                    else
                    {
                        resetButton.Enabled = false;
                    }
                }
            }
        }

        internal static string getLoggedOnUserFromHostname(string hostName, bool showConnectionError)
        {
            if (hostName != null && hostName != "")
            {
                using (Ping p = new Ping())
                {
                    if (p.Send(hostName).Status != IPStatus.Success) return "";
                }

                ConnectionOptions options = new ConnectionOptions();
                options.Impersonation = System.Management.ImpersonationLevel.Impersonate;

                ManagementScope scope = new ManagementScope("\\\\" + hostName + "\\root\\cimv2");
                try
                {

                    scope.Connect();

                    //Query system for Operating System information
                    ObjectQuery query = new ObjectQuery("SELECT UserName FROM Win32_ComputerSystem");
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

                    ManagementObjectCollection queryCollection = searcher.Get();
                    string username = (string)queryCollection.Cast<ManagementBaseObject>().First()["UserName"];
                    return username;
                    /*foreach (ManagementObject m in queryCollection)
                    {
                        
                    }*/
                }
                catch (Exception exc)
                {
                    if (showConnectionError)
                    {
                        MessageBox.Show(hostName + " was unable to be connected to. Please ensure the hostname is entered correctly and that the device is online.", "Unable to Connect to " + hostName);
                    }
                }
            }

            return "";
        }

        internal static ComputerInformation getAllComputerInformationFromHostName(string hostName, bool showConnectionError)
        {
            ComputerInformation ci = new ComputerInformation();
            if (hostName != null && hostName != "")
            {
                ci.ComputerName = hostName;
                using (Ping p = new Ping())
                {
                    if (p.Send(hostName).Status != IPStatus.Success) return ci;
                }

                ConnectionOptions options = new ConnectionOptions();
                options.Impersonation = System.Management.ImpersonationLevel.Impersonate;

                ManagementScope scope = new ManagementScope("\\\\" + hostName + "\\root\\cimv2");
                try
                {

                    scope.Connect();

                    //Query system for Operating System information
                    ObjectQuery query = new ObjectQuery("SELECT UserName FROM Win32_ComputerSystem");
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

                    ManagementObjectCollection queryCollection = searcher.Get();
                    ci.LoggedOnUserName = (string)queryCollection.Cast<ManagementBaseObject>().First()["UserName"];


                    ObjectQuery biosQuery = new ObjectQuery("SELECT SerialNumber FROM Win32_BIOS");
                    ManagementObjectSearcher biosSearcher = new ManagementObjectSearcher(scope, biosQuery);

                    ManagementObjectCollection biosQueryCollection = biosSearcher.Get();
                    ci.ServiceTag = (string)queryCollection.Cast<ManagementBaseObject>().First()["serialnumber"];
                }
                catch (Exception exc)
                {
                    if (showConnectionError)
                    {
                        MessageBox.Show(hostName + " was unable to be connected to. Please ensure the hostname is entered correctly and that the device is online.", "Unable to Connect to " + hostName);
                    }
                }
            }

            return ci;
        }
    }
}


public class Rootobject
{
    public Assetwarrantyresponse[] AssetWarrantyResponse { get; set; }
    public Invalidformatassets InvalidFormatAssets { get; set; }
    public Invalidbilassets InvalidBILAssets { get; set; }
    public Excesstags ExcessTags { get; set; }
    public object AdditionalInformation { get; set; }
}

public class Invalidformatassets
{
    public object[] BadAssets { get; set; }
}

public class Invalidbilassets
{
    public object[] BadAssets { get; set; }
}

public class Excesstags
{
    public object[] BadAssets { get; set; }
}

public class Assetwarrantyresponse
{
    public Assetheaderdata AssetHeaderData { get; set; }
    public Productheaderdata ProductHeaderData { get; set; }
    public Assetentitlementdata[] AssetEntitlementData { get; set; }
}

public class Assetheaderdata
{
    public string BUID { get; set; }
    public string ServiceTag { get; set; }
    public DateTime ShipDate { get; set; }
    public string CountryLookupCode { get; set; }
    public string LocalChannel { get; set; }
    public string CustomerNumber { get; set; }
    public string ItemClassCode { get; set; }
    public bool IsDuplicate { get; set; }
    public string MachineDescription { get; set; }
    public string OrderNumber { get; set; }
    public object ParentServiceTag { get; set; }
}

public class Productheaderdata
{
    public string SystemDescription { get; set; }
    public string ProductId { get; set; }
    public string ProductFamily { get; set; }
    public string LOB { get; set; }
    public string LOBFriendlyName { get; set; }
}

public class Assetentitlementdata
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string ServiceLevelDescription { get; set; }
    public string ServiceLevelCode { get; set; }
    public int ServiceLevelGroup { get; set; }
    public string EntitlementType { get; set; }
    public string ServiceProvider { get; set; }
    public string ItemNumber { get; set; }
}