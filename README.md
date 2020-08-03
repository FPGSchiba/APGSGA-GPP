<h1>Gast-Zugang erstellen</h1>

<p>Einleitung -> tbd</p>

<h4>Wichtiges</h4>

<p>Um das App und all seine Feature richtig Nutzen zu können muss man Zugang zum LAN von APG haben.
Ansonsten kann man das App und seine Features nicht nutzen. </p>

<p>Auch ist es von Vorteil einen Drucker zu ahben,
denn ohne Drucker muss man sich den Benutzer und sein Passwort von selber aufschreiben oder es sich merken.
Das Programm Druckt automatisch über den Durcker, der als Standard Drucker für das Gerät, aufdem das Programm installiert ist.</p>

<p>Auch braucht man, um sich bei diesem App einzuloggen einen Benutzer und ein Passwort.
Dieser Benutzername und Passwort sollten Sie erhalten haben, wenn nicht schauen Sie im 
APG Wiki nach, dort sollte eine Anleitung zum erstellen eines Gast-Benutzer vorhanden sein.</p>

<h2>Installation</h2>

<h4>Über Cluebiz</h4>

<ul>
<li>Cluebiz?</li>
</ul>

<h4>Über GitHub-Releases</h4>

<p>Eine andere Variante ist es Unter "Releases" den neusten Release vom Gast-Zugang App herunterzuladen,
 im Downloads Ordner die Datei "Publish.zip" zu entpacken und im Ordner die Datei "setup.exe" auszuführen.</p>

<h4>Über GitHub Code</h4>

<p>Einea andere Variante ist es den kompletten Code herunter zu laden und das File "Publish.zip"
 im Ordner "APGSGA-GPP_App" zu entpacken und dort die datei "setup.exe" auf Ihrem Windows PC
 laufen zu lassen, indem Sie auf die Datei Doppelclicken.</p>

<h2>Anleitung</h2>
<ol>

<li>

Starten Sie die APP: <code>APGSGA-GPP_APP</code>

<div style="text-align:left"><img src=".\bilder\APGSGA_APP.jpg" alt="APGSGA-GPP_APP"/></div>

</li>

<li>

Melden Sie sich mit Ihren Anmelde Daten an.

<div style="text-align:left"><img src=".\bilder\login.png" alt="Form: login.cs"/></div>

</li>

<li>

Geben Sie den Benutzernamen und Passwort ein, falls Sie etwas an dem Vorgeschlagenem Benutzer ändern wollen.

<div style="text-align:left"><img src=".\bilder\form1_Benutzer.png" alt="Form: Form1.cs Benutzer markiert"/></div>

</li>

<li>

Wählen Sie das Datum aus, von wann bis wann der Benutzer aktiv sein soll.

<div style="text-align:left"><img src=".\bilder\form1_Date.png" alt="Form: Form1.cs Datum markiert"/></div>

</li>

<li>

Drücken Sie auf Create and Print.

<div style="text-align:left"><img src=".\bilder\form1_Create.png" alt="Form: Form1.cs Create markiert"/></div>

</li>

<li>Das Dokument mit den Anmelde Daten wird jetzt in Ihrem Standard-Drucker gedruckt.</li>

<li>Geben Sie das Dokument Ihrem Gast.</li>

</ol>

<h2>Dokumentation</h2>

<p>Die Dokumentation ist eine Übersicht der Files und Funktionen, welche ich in diesem Projekt entwickelt habe.
 Ich empfehle, jedem, der wissen will, wie das Programm Funktioniert und eine Grundlegende Ahnung hat von Programmieren
 diese Dokumentation zu lesen. Es ist kein muss diese Dokumentation zu lesen, wenn man das know how nicht hat oder keine lust dazu.</p>

<h3>Grundlegendes</h3>

<p>Dieses App wude mithilfe von Visual Studio 2017, als eine Windows-Forms Applikation entwickelt und besitzt 3 Forms und 3 andere Classes.</p>

<h4>Die Forms:</h4>

<ol>
<li>Form1 - Das eigentliche Hauptprogramm, also das Main-GUI der Applikation</li>

<li>login - Das, wie der Name schon sagt, Login-GUI</li>

<li>Form2 - Das GUI, für die Daten-Anzeige, also dieses GUI wird im moment (Stand 24.07.2020) nicht benutzt dient aber, um die erstellten Benutzer anzeigen zu können.</li>
</ol>

<h4>Die Klassen:</h4>

<ol>
<li>Program - Der Eintrittspunkt der Applikation, also das haupt Script.</li>

<li>User - Eine Klasse zum Aufteilen von Daten, also eine Klasse, die nur Benutzer Daten erstellt und Speichert.</li>

<li>LoginData - Ähnlich, wie User einfach diesmal für die Login-Informationen</li>
</ol>

<h3>Scripts</h3>

<h4>Allgemeiner Ablauf</h4>

<p>Das Script Program.cs ist der Eintritts Punkt der Applikation. Um mit Windows-Forms arbeiten zu können, müssen die Forms in einen Loop eingebettet werden.
Dieses Script öffnet als erstes natürlich den <code>loginLoop</code>, also öffnet es in einem Thread die Form <code>login.cs</code>:</p>

<pre><code>        static void Main(string[] args)
        {
            //Make the Login-Loop
            Thread thread = new Thread(loginLoop);
            thread.Start();
        }
</code></pre>

<div style="text-align:center"><img src=".\bilder\login.png" alt="Form: login.cs"/></div>

<p>Wenn der Benutzer dann den Benutzernamen und das Passwort eingegeben hat sendet die Form <code>login.cs</code> den eingegebenen Benutzernamen und das Passwort zurück ans <code>Program.cs</code>.
Dort wird mithilfe der Daten von der Klasse <code>LoginData</code>, welche alle Benutzernamen und Passwörter in einem <code>Dictionary</code> festhaltet. 
Wenn dann der Login Validiert ist startet die Appliaktion die Form <code>Form1</code>, als Hauptfenster der Applikation. </p>

<div style="text-align:center"><img src=".\bilder\form1.png" alt="Form: form1.cs" /></div>

<p>Wenn die Form dann gstartet ist, was immer etwa 2 Sekunden braucht generiert das Script <code>Program.cs</code> schon den ersten Benutzer, wenn ich von
Benutzer rede, meine ich immer einen Benutzernamen und ein Passwort, dass die Klasse <code>User.cs</code> bereitstellt. Unterhalb des Users sieht man auch
eine Zeitspanne, also zwei <code>DatePicker</code>, eines Von und das andere Bis.</p>

<p>Wenn man jetzt einen Benutzer erstellen möchte Drückt man auf den Button Create and Print:</p>

<div style="text-align:left"><img src=".\bilder\form1_Create.png" alt="Form: Form1.cs Create markiert"/></div>

<p>Dann werden 2 Threads generiert, auf  dem ersten wird ein Querry-String generiert, der z.B. so aussehen kann: <code>local-userdb-guest add username "TestUser" password "pjox7856" start-time 07/08/2020 16:29 expiry time 07/08/2020 18:00</code>
mit diesem Querry-String wird ein neuer Benutzer generiert, der <code>TestUser</code> heisst, mit dem passwort <code>pjox7856</code>, der am <code>07/08/2020 16:29</code>
gültig wird und am <code>07/08/2020 18:00</code> wieder ungültig ist. Auf dem 2. Thread wird dann gedruckt, also mithilfe der Microsoft.Office.Interop.Word
Library wird ein Word generiert, dass so aussieht:</p>

<div style="text-align:left"><img src=".\bilder\Word.png" alt="Ouput Word"/></div>

<h4>Program.cs</h4>

<p>Die Klasse <code>Program.cs</code> besteht grob gesagt aus 6 Punkten:</p>

<ol>

<li>Variablen: 
    <ul>
        <li><code>public static string password</code> - das Passwort</li>
        <li><code>public static string username</code> - Der Benutzername</li>
        <li><code>public static string host</code> - Die Server IP</li>
        <li><code>public static string user</code> - Der Benutzername für den Server</li>
        <li><code>public static string pwd</code> - Das Passwort für den Server</li>
        <li><code>public static string Gast</code> - Das Standortkürzel</li>
        <li><code>public static LoginData login</code> - Die Klasse LoginData</li>
        <li><code>public static string localXML</code> - Der Pfad zum Lokalen XML</li>
    </ul>
</li>

<li>Main: 
    <ul>
        <li><code>Main(string[] args)</code> - Startet den <code>loginLoop()</code></li>
    </ul>
</li>

<li>Loops:
    <ul><code>MainLoop()</code> - Der Loop für die Form1, also das Hauptprogramm.</ul>
    <ul><code>showLoop()</code> - Der Loop für die Form2, also der Datenanzeige, die im Moment nicht verwendet wird (stand 03.08.2020)</ul>
    <ul><code>loginLoop()</code> - Der Loop für die Form Login, also das Login Fenster.</ul>
</li>

<li>Querry:
    <ul>    
        <li><code>sendData(string Data)</code> - Die Funktion, um einen Querry-String auf dem Server auszuführen, mithilfe von Passwort und Benutzernamen vom Log In.</li>
    </ul>
</li>

<li>Login Validation:
    <ul>
        <li><code>MD5Hash(string input)</code> - Ist ein String, der aus einem Input String einen MD5-Hash macht, indem er den string in Bytes übersetzt diese Hashed und dann zurück übersetzt.</li>
        <li><code>validateLogin(string password, string username)</code> - Die Funktion, die Mithilfe der Klasse <code>LoginData.cs</code> das Login in der Form: <code>Login.cs</code> überprüft. Zum Abschluss Initialisiert diese Funktion dann auch noch den <code>MainLoop()</code>.</li>
    </ul>
</li>

<li>Init MainLoop:
    <ul>
        <li><code>startMainLoop()</code> - Eine Funktion die das Standortkürzel aus dem Computernamen holt und in der Variable <code>string Gast</code> das Kürzel speichert. Auch generiert diese Funktion dann einen Benutzer und Debuged gleich das Datum, also die Funktion <code>debugMinDate()</code> von der Form: <code>Form1.cs</code> aufruft.</li>
    </ul>
</li>

</ol>

<h4>User.cs</h4>

<p><code>User.cs</code> ist eine Klasse, um Daten zu genereieren, die man für ein Login braucht, also einen Benutzernamen und ein Passwort.
Diese Klasse macht wirklich nicht mehr, als diese beiden Dinge zu speichern und zu generieren.</p>

<ol>

<li>Variablen - einen <code>public</code> username und ein <code>public</code> password</li>

<li>Generation:
    <ul>
        <li><code>CreatePW()</code> - Ein String, der aus der variable <code>const string chars = ""</code> random eines der Zeichen, im String auswählt und dann mit der länge einen neuen String generiert, der dann aus den Random ausgewählten Zeichen besteht.</li>
        <li><code>CreateUsername()</code> - Ein String, der aus dem Datum, welches im <code>DatePicker</code> steht und der Standort Bezeichnung des Rechners einen Benutzernamen generiert.</li>
    </ul>
</li>

</ol>

<h4>LoginData.cs</h4>

<p><code>LoginData.cs</code> ist eine Klasse, wie <code>User.cs</code>, die nur für die Daten speicherung und genereation verantwortlich ist, um genau zu sein, für die Login-Daten.</p>

<ol>

<li>Variablen - einen <code>public public Dictionary&lt;string, string></code> dic_login, also ein Dictionary mit dem Key als Benutzernamen und dem Value als Passwort.</li>

<li>Generation:

<ul>
    <li><code>CreateValidation()</code> - ein <code>Dictionary&lt;string, string></code>, der  die Benutzernamen und MD5 gehashte passwörter speichert.</li>
</ul>

</li>

</ol>

<h4>form1.cs</h4>

<p>Das ist das eigentliche Programm, also die Haupt-Form. In dieser Form macht man neue Benutzer und Druckt diese.</p>

<ol>

<li>Variablen: 
    <ul>
        <li><code>private string fileName</code> ist die Variable, um das Word-Dokument zu speichern.</li>
        <li><code>private Microsoft.Office.Interop.Word.Application word</code> das ist die Variable, um ein Objekt zu generieren, mitdem man die Word Applikation ausführen kann.</li>
        <li><code>private Microsoft.Office.Interop.Word.Document doc;</code> das ist die Variable, um ein Objekt zu generieren, mitdem man ein Word erstellen kann.</li>
    </ul>
</li>

<li>Init - Ist die Initialisierung, des Codes, der von dem Designer erstellt wurde. Also wird hier das ganze visuelle Initialisiert.</li>

<li>Users:
    <ul>
        <li><code>Adduser()</code> - Eine Funktion, die einen neuen Benutzer generiert, also die Klasse <code>User</code> erstellt und dann in die Text-Felder schreibt.</li>
        <li><code>User_Gene_Click(object sender, EventArgs e)</code> - ist eine Funktion, die einen neuen Benutzernamen generiert und in das Text-Fled hineinschreibt.</li>
        <li><code>PW_Gene_Click(object sender, EventArgs e)</code> - ist eine Funktion, die ein neues Passwort generiert und das in das Text-Feld hineinschreibt.</li>
    </ul>
</li>

<li>Create User:
    <ul>
        <li><code>Create_B_Click(object sender, EventArgs e)</code> - Erstellt den Querry-String, der dann auf dem Server ausgeführt wird, um einen Zugang zu erstellen. Dann ertsellt die Funktion 2 Threads, um 1. den Querry-String zu senden und 2. das ganze Auszudrucken.</li>
        <li><code>formatTime(string Time, bool isBis)</code> - Formatiert das Datum richtig, damit der String beim Server auch erkannt wird.</li>
        <li><code>formatMonth(string month)</code> - Das ist eine Funktion die den Monat, der als String angegeben ist, also z.B. Juli, zu einem Int umwandelt.</li>
    </ul>
</li>

<li>Handling Time:
    <ul>
        <li><code>dTP_Von_ValueChanged(object sender, EventArgs e)</code> - Das ist eine Funktion, die Aufgerufen wird, wenn man das Von-Datum ändert und bewirkt, dass ein neuer Benutzer mit dem neuen Dateum erstellt wird. Auch setzt es das <code>minDate</code> vom Bis-Datum neu, sowie auch das Bis-Datum selber.</li>
        <li><code>dTP_Von_GetTime()</code> - Eine Funktion, die Benötigt wird, um einen Benutzer zu erstellen, denn sie nimmt das Von-Datum und Formatiert es gleich richtig.</li>
        <li><code>debugMinDate()</code> - Debugged das erste einloggen in Form1  so, dass man nicht weiter zurück kann mit Bis als das Von-Datum.</li>
    </ul>
</li>

<li>Print Handling:
    <ul>
        <li><code>print(string username, string password)</code> - Diese Methode erstellt das Word-Dokument, welches dann zum Drucken verwendet wird und leitet dann die Druckmethode ein.</li>
        <li><code>printDocument()</code> - Diese Methode Druckt das Word-Dokument.</li>
    </ul>
</li>
</ol>

<h4>form2.cs</h4>

<p>Eine im moment (Stand: 03.08.2020) nicht verwendete Form, die zur veranschaulichung der Benutzer dienen soll.</p>

<ol>

<li>Init - Ist die Initialisierung, des Codes, der von dem Designer erstellt wurde. Also wird hier das ganze visuelle Initialisiert.</li>

<li>load XML:
    <ul>
        <li><code>dataSet1_Initialized(object sender, EventArgs e)</code> - Ist die Funktion, die aufgerufen wird, wenn das <code>dataSet1</code> Initialisiert wird und lädt das XML in die tabelle hinein.</li>
        <li><code>formatXML(string content)</code> - Diese Funktion Formatiert das XML so, dass es auch für nicht INformatiker leserlich ist.</li>
    </ul>
</li>

<li>Buttons:
    <ul>
        <li><code>dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)</code> - Diese Funktion wird aufgerfuen, wenn man auf einen der Daten in der tabelle drückt, also ist das die Button-Funktion. Denn diese Funktion schaut, wo gelicked wurde und führt aktionen aus, wenn man auf die Knöpfe gedrückt hat. Also z.B. erkennt sie, wenn man auf den Löschen Knopf drückt und dann löscht sie den Benutzer oder wenn man auf den Drucken Knopf drückt Druckt sie den Benutzer.</li>
    </ul>
</li>

</ol>

<h4>Login.cs</h4>

<p>Das ist die Form, welche verantwortlich ist, für das Log In.</p>

<ol>

<li>Init - Ist die Initialisierung, des Codes, der von dem Designer erstellt wurde. Also wird hier das ganze visuelle Initialisiert.</li>

<li> Login-Send:
    <ul>
        <li><code>button1_Click(object sender, EventArgs e)</code> - Ist die Funktion, wenn man auf Login Drückt. Sie liest den Benutzernamen und das Passwort hinaus und sendet die Daten an die Validier Funktion im <code>Program.cs</code>.</li>
        <li><code>login_KeyDown(object sender, KeyEventArgs e)</code> - Ist genau das gleiche wie: <code>button1_Click(object sender, EventArgs e)</code>, einfach wird das hier ausgelöst, wenn man [enter] drückt.</li>
    </ul>
</li>

<li>reset:
    <ul>
        <li><code>resetLogin()</code> - Ist eine Funktion, die die Eingabe-Felder leert, wenn man der Log In gescheitert ist.</li>
    </ul>
</li>

</ol>