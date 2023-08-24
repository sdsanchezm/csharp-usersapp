namespace UserStore.Models 
{
  public class User
  {
      public int Id { get; set; }
      public string? Firstname { get; set; }
      public string? Lastname { get; set; }
      public string? About { get; set; }
      public string? Username { get; set; }
      public int? Usernumber { get; set; }
      public string? City { get; set; }
      public string? Province { get; set; }
      public string? Country { get; set; }

  }
}