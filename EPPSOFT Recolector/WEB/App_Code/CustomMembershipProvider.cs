using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Security;
using Entidades;
using AccesoDatos;

/// <summary>
/// Summary description for CustomMembershipProvider
/// </summary>
public class CustomMembershipProvider : MembershipProvider
{
    #region members
    private const int newPasswordLength = 8;
    private string applicationName;

    private bool enablePasswordReset;
    private bool enablePasswordRetrieval;
    private MachineKeySection machineKey; // Used when determining encryption key values.
    private int maxInvalidPasswordAttempts;
    private int minRequiredNonAlphanumericCharacters;
    private int minRequiredPasswordLength;
    private int passwordAttemptWindow;
    private MembershipPasswordFormat passwordFormat;
    private string passwordStrengthRegularExpression;
    private bool requiresQuestionAndAnswer;
    private bool requiresUniqueEmail;
    #endregion

    #region properties
    /// <summary>
    /// Indicates whether the membership provider is configured to allow users to retrieve their passwords.
    /// </summary>
    /// <returns>
    /// true if the membership provider is configured to support password retrieval; otherwise, false. The default is false.
    /// </returns>
    public override bool EnablePasswordRetrieval
    {
        get { return enablePasswordRetrieval; }
    }

    /// <summary>
    /// Indicates whether the membership provider is configured to allow users to reset their passwords.
    /// </summary>
    /// <returns>
    /// true if the membership provider supports password reset; otherwise, false. The default is true.
    /// </returns>
    public override bool EnablePasswordReset
    {
        get { return enablePasswordReset; }
    }

    /// <summary>
    /// Gets a value indicating whether the membership provider is configured to require the user to answer a password question for password reset and retrieval.
    /// </summary>
    /// <returns>
    /// true if a password answer is required for password reset and retrieval; otherwise, false. The default is true.
    /// </returns>
    public override bool RequiresQuestionAndAnswer
    {
        get { return requiresQuestionAndAnswer; }
    }

    /// <summary>
    /// The name of the application using the custom membership provider.
    /// </summary>
    /// <returns>
    /// The name of the application using the custom membership provider.
    /// </returns>
    public override string ApplicationName { get; set; }

    /// <summary>
    /// Gets the number of invalid password or password-answer attempts allowed before the membership user is locked out.
    /// </summary>
    /// <returns>
    /// The number of invalid password or password-answer attempts allowed before the membership user is locked out.
    /// </returns>
    public override int MaxInvalidPasswordAttempts
    {
        get { return maxInvalidPasswordAttempts; }
    }

    /// <summary>
    /// Gets the number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
    /// </summary>
    /// <returns>
    /// The number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
    /// </returns>
    public override int PasswordAttemptWindow
    {
        get { return passwordAttemptWindow; }
    }

    /// <summary>
    /// Gets a value indicating whether the membership provider is configured to require a unique e-mail address for each user name.
    /// </summary>
    /// <returns>
    /// true if the membership provider requires a unique e-mail address; otherwise, false. The default is true.
    /// </returns>
    public override bool RequiresUniqueEmail
    {
        get { return requiresUniqueEmail; }
    }

    /// <summary>
    /// Gets a value indicating the format for storing passwords in the membership data store.
    /// </summary>
    /// <returns>
    /// One of the <see cref="T:System.Web.Security.MembershipPasswordFormat" /> values indicating the format for storing passwords in the data store.
    /// </returns>
    /// 
    public override MembershipPasswordFormat PasswordFormat
    {
        get { return passwordFormat; }
    }

    /// <summary>
    /// Gets the minimum length required for a password.
    /// </summary>
    /// <returns>
    /// The minimum length required for a password. 
    /// </returns>
    public override int MinRequiredPasswordLength
    {
        get { return minRequiredPasswordLength; }
    }

    /// <summary>
    /// Gets the minimum number of special characters that must be present in a valid password.
    /// </summary>
    /// <returns>
    /// The minimum number of special characters that must be present in a valid password.
    /// </returns>
    public override int MinRequiredNonAlphanumericCharacters
    {
        get { return minRequiredNonAlphanumericCharacters; }
    }

    /// <summary>
    /// Gets the regular expression used to evaluate a password.
    /// </summary>
    /// <returns>
    /// A regular expression used to evaluate a password.
    /// </returns>
    public override string PasswordStrengthRegularExpression
    {
        get { return passwordStrengthRegularExpression; }
    }

    #endregion

    #region public methods
    /// <summary>
    /// Initialize this membership provider. Loads the configuration settings.
    /// </summary>
    /// <param name="name">membership provider name</param>
    /// <param name="config">configuration</param>
    public override void Initialize(string name, NameValueCollection config)
    {
        // Initialize values from web.config.
        if (config == null) throw new ArgumentNullException("config");

        if (String.IsNullOrEmpty(name)) name = "CustomMembershipProvider";

        if (String.IsNullOrEmpty(config["description"]))
        {
            config.Remove("description");
            config.Add("description", "Hitos Custom Membership Provider");
        }

        // Initialize the abstract base class.
        base.Initialize(name, config);

        applicationName = GetConfigValue(config["applicationName"], HostingEnvironment.ApplicationVirtualPath);
        maxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
        passwordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
        minRequiredNonAlphanumericCharacters = Convert.ToInt32(GetConfigValue(config["minRequiredNonAlphanumericCharacters"], "1"));
        minRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "7"));
        passwordStrengthRegularExpression = Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], ""));
        enablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
        enablePasswordRetrieval = Convert.ToBoolean(GetConfigValue(config["enablePasswordRetrieval"], "true"));
        requiresQuestionAndAnswer = Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "false"));
        requiresUniqueEmail = Convert.ToBoolean(GetConfigValue(config["requiresUniqueEmail"], "true"));
        //passwordFormat = MembershipPasswordFormat.Clear;

        string temp_format = config["passwordFormat"] ?? "Hashed";

        switch (temp_format)
        {
            case "Hashed":
                passwordFormat = MembershipPasswordFormat.Hashed;
                break;
            case "Encrypted":
                passwordFormat = MembershipPasswordFormat.Encrypted;
                break;
            case "Clear":
                passwordFormat = MembershipPasswordFormat.Clear;
                break;
            default:
                throw new ProviderException("Password format not supported.");
        }

        // Get encryption and decryption key information from the configuration.
        Configuration cfg = WebConfigurationManager.OpenWebConfiguration(HostingEnvironment.ApplicationVirtualPath);
        machineKey = (MachineKeySection)cfg.GetSection("system.web/machineKey");

        if (machineKey.ValidationKey.Contains("AutoGenerate"))
            if (PasswordFormat != MembershipPasswordFormat.Clear) throw new ProviderException("Hashed or Encrypted passwords are not supported with auto-generated keys.");
    }



    /// <summary>
    /// Verifies that the specified user name and password exist in the data source.
    /// </summary>
    /// <returns>true if the specified username and password are valid; otherwise, false.</returns>
    /// <param name="username">The name of the user to validate.</param>
    /// <param name="password">The password for the specified user.</param>
    public override bool ValidateUser(string username, string password)
    {
        bool isValid = false;

        Entidades.EPPSOFTRecolectorEntities context = Entidades.EPPSOFTRecolectorEntities.Context;
        IQueryable<Usuario> users = from u in context.Usuario
                                    where u.NombreUsuario == username //&& u.ApplicationName == applicationName
                                    select u;

        if (users.Count() != 1) return false;

        Usuario user = users.First();
        if (user == null) return false;

        if (CheckPassword(password, user.Contrasenia))
        {
            //Validacion de cuenta aprobada y no bloqueada
            if (user.Habilitado)
            {
                isValid = true;
                context.SaveChanges();
            }
        }


        return isValid;
    }

    /// <summary>
    /// Gets information from the data source for a user. Provides an option to update the last-activity date/time stamp for the user.
    /// </summary>
    /// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object populated with the specified user's information from the data source.</returns>
    /// <param name="username">The name of the user to get information for.</param>
    /// <param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user.</param>
    public override MembershipUser GetUser(string username, bool userIsOnline)
    {
        MembershipUser membershipUser = null;

        Entidades.EPPSOFTRecolectorEntities context = Entidades.EPPSOFTRecolectorEntities.Context;
        IQueryable<Usuario> users = from u in context.Usuario
                                    where u.NombreUsuario == username //&& u.ApplicationName == applicationName
                                    select u;

        if (users.Count() == 1)
        {
            Usuario user = users.First();
            if (user != null)
            {
                membershipUser = GetMembershipUserFromPersitentObject(user);
            }
        }

        return membershipUser;
    }

    #endregion

    #region private methods
    /// <summary>
    /// A helper function that takes the current persistent user and creates a CustomMembershiUserEntity from the values.
    /// </summary>
    /// <param name="user">user object containing the user data retrieved from database</param>
    /// <returns>membership user object</returns>
    private MembershipUser GetMembershipUserFromPersitentObject(Usuario user)
    {

        if (!string.IsNullOrEmpty(Name))

            return new MembershipUser(  Name,
                                        user.NombreUsuario,
                                        user, 
                                        string.Empty, 
                                        string.Empty, 
                                        string.Empty, 
                                        true, 
                                        true, 
                                        DateTime.Now, 
                                        DateTime.Now, 
                                        DateTime.Now, 
                                        DateTime.Now, 
                                        DateTime.Now);
        else
            return null;
    }



    /// <summary>
    /// Compares password values based on the MembershipPasswordFormat.
    /// </summary>
    /// <param name="password">password</param>
    /// <param name="dbpassword">database password</param>
    /// <returns>whether the passwords are identical</returns>
    private bool CheckPassword(string password, string dbpassword)
    {
        string pass1 = password;
        string pass2 = dbpassword;

        switch (PasswordFormat)
        {
            case MembershipPasswordFormat.Encrypted:
                pass2 = UnEncodePassword(dbpassword);
                break;
            case MembershipPasswordFormat.Hashed:
                pass1 = EncodePassword(password);
                break;
            default:
                break;
        }

        return pass1 == pass2;
    }

    /// <summary>
    /// Encrypts, Hashes, or leaves the password clear based on the PasswordFormat.
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    private string EncodePassword(string password)
    {
        string encodedPassword = password;

        switch (PasswordFormat)
        {
            case MembershipPasswordFormat.Clear:
                break;
            case MembershipPasswordFormat.Encrypted:
                encodedPassword = Convert.ToBase64String(EncryptPassword(Encoding.Unicode.GetBytes(password)));
                break;
            case MembershipPasswordFormat.Hashed:
                HMACSHA1 hash = new HMACSHA1 { Key = HexToByte(machineKey.ValidationKey) };
                encodedPassword = Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
                break;
            default:
                throw new ProviderException("Unsupported password format.");
        }

        return encodedPassword;
    }

    public  string CodificarContraseña(string password)
    {
        Configuration cfg = WebConfigurationManager.OpenWebConfiguration(HostingEnvironment.ApplicationVirtualPath);
        machineKey = (MachineKeySection)cfg.GetSection("system.web/machineKey");
        HMACSHA1 hash = new HMACSHA1 { Key = HexToByte(machineKey.ValidationKey) };
        string encodedPassword = Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));

        return encodedPassword;
    }

    /// <summary>
    /// Decrypts or leaves the password clear based on the PasswordFormat.
    /// </summary>
    /// <param name="encodedPassword"></param>
    /// <returns></returns>
    private string UnEncodePassword(string encodedPassword)
    {
        string password = encodedPassword;

        switch (PasswordFormat)
        {
            case MembershipPasswordFormat.Clear:
                break;
            case MembershipPasswordFormat.Encrypted:
                password = Encoding.Unicode.GetString(DecryptPassword(Convert.FromBase64String(password)));
                break;
            case MembershipPasswordFormat.Hashed:
                throw new ProviderException("Cannot unencode a hashed password.");
            default:
                throw new ProviderException("Unsupported password format.");
        }

        return password;
    }

    /// <summary>
    /// Converts a hexadecimal string to a byte array. Used to convert encryption key values from the configuration.
    /// </summary>
    /// <param name="hexString"></param>
    /// <returns></returns>
    private static byte[] HexToByte(string hexString)
    {
        byte[] returnBytes = new byte[hexString.Length / 2];
        for (int i = 0; i < returnBytes.Length; i++)
        {
            returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
        }
        return returnBytes;
    }

    /// <summary>
    /// A helper function to retrieve config values from the configuration file.
    /// </summary>
    /// <param name="configValue"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    private static string GetConfigValue(string configValue, string defaultValue)
    {
        return String.IsNullOrEmpty(configValue) ? defaultValue : configValue;
    }
    #endregion


    public override bool ChangePassword(string username, string oldPassword, string newPassword)
    {
        if (!ValidateUser(username, oldPassword)) return false;

        //notify that password is going to change
        ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, newPassword, true);
        OnValidatingPassword(args);

        if (args.Cancel)
        {
            if (args.FailureInformation != null) throw args.FailureInformation;
            throw new MembershipPasswordException("Change password canceled due to new password validation failure.");
        }

        Entidades.EPPSOFTRecolectorEntities context = Entidades.EPPSOFTRecolectorEntities.Context;
        IQueryable<Usuario> users = from u in context.Usuario
                                    where u.NombreUsuario == username //&& u.ApplicationName == applicationName
                                    select u;

        if (users.Count() != 1) throw new ProviderException("Change password failed. No unique user found.");

        Usuario user = users.First();
        user.Contrasenia= EncodePassword(newPassword);
        user.ContraseniaAsignada = false;

        try
        {
            context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
    {
        throw new NotImplementedException();
    }

    public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
    {
        throw new NotImplementedException();
    }

    public override bool DeleteUser(string username, bool deleteAllRelatedData)
    {
        throw new NotImplementedException();
    }

    public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
        throw new NotImplementedException();
    }

    public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
        throw new NotImplementedException();
    }

    public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
    {
        throw new NotImplementedException();
    }

    public override int GetNumberOfUsersOnline()
    {
        throw new NotImplementedException();
    }

    public override string GetPassword(string username, string answer)
    {
        throw new NotImplementedException();
    }

    public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
    {
        throw new NotImplementedException();
    }

    public override string GetUserNameByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public override string ResetPassword(string username, string answer)
    {
        throw new NotImplementedException();
    }

    public override bool UnlockUser(string userName)
    {
        throw new NotImplementedException();
    }

    public override void UpdateUser(MembershipUser user)
    {
        throw new NotImplementedException();
    }
}