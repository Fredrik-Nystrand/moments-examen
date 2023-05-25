# moments-examen

Backend:
Connection string finns redan definerad i appsettings.json och behöver inte uppdateras.
Det är bara att starta upp projektet i Visual Studio som vanligt för att starta backend.

Frontend:
CD:a in till frontend mappen och starta med "npm run dev"

Det finns ett test konto att logga in med.
Användarnamn: test@mail.com
Lösenord: 1234

Det går inte att registrera ett nytt konto, om du vill göra det får du göra det via swagger eller via postman.


Om appsettings av någon anledning inte skulle ha rätt connectionstring så kan du kopiera den nedan:
Server=tcp:feu21-fn.database.windows.net,1433;Initial Catalog=Moments;Persist Security Info=False;User ID=serveradmin;Password=BytMig123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
