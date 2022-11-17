using System;
using System.Security.Principal;
using Microsoft.Win32;

namespace PolicyOverride
{
  internal class Program
  {
    static void Main(string[] args)
    {
      try
      {

        if (!IsAdmin())
        {
          Console.WriteLine("This program must be run as admin.");
          Console.WriteLine("Press <Enter> to exit.");
          Console.ReadLine();
          return;
        }
        UpdateReg(Registry.LocalMachine, @"SOFTWARE\Policies\Google\Chrome", "PasswordManagerEnabled", 1);
        UpdateReg(Registry.LocalMachine, @"SOFTWARE\Policies\Google\Chrome", "PasswordManagerAllowShowPasswords", 1);
        Console.WriteLine("Completed.");
        Console.WriteLine("Press <Enter> to exit.");
        Console.ReadLine();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }
    private static bool IsAdmin()
    {
      try
      {
        using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
        {
          return (new WindowsPrincipal(identity)).IsInRole(WindowsBuiltInRole.Administrator);
        }
      }
      catch
      {
        return false;
      }
    }

    private static void UpdateReg(RegistryKey root, string path, string key, int value)
    {
      RegistryKey rk = root.OpenSubKey(path, true);
      rk.SetValue(key, value);
    }
  }
}
