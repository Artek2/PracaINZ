namespace ExpenseTracker
{
  public class AuthenticationSettings
  {
    public string JwtKey { get; set; } //ciąg znaków, służący jako klucz do tworzenia i sprawdzania podpisu tokena JWT (JSON Web Token).
    public int JwtExpireDays { get; set; } // liczba całkowita reprezentująca liczbę dni, na które token JWT powinien być ważny po jego zgenerowaniu.
    public string JwtIssuer { get; set; } //ciąg znaków, określający nadawcę tokena JWT.
  }
}
