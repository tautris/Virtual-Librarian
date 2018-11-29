# Virtual-Librarian
Applied Object-Oriented Programming

**Native iOS and Android Application, allowing users to read eBooks in PDF Format.**

##### Tasks for 12/04
 - Implement Front End design pattern, to separate logic from views (e. g. MVP)
 - Implement Front End Face Detection (Google ML Kit)
 
#### Tasks for 12/11
 - Implement DataBase using Entity Framework
 - Draw Entity Relationship diagram
 - Make sure, that App works in iOS
 - *//TODO:*

### How to - Back End:
 - Install all required dependencies from nuget packet manager
 - Change project's URL from localhost:port -> 127.0.0.1:port (e. g. 127.0.0.1:50863)
 - Open "../Virtual Librarian/.vs/config/applicationhost.config" and bindignInformation of most recent Site (by ID) to "*:port:*" (e. g. bindingInformation=* :50863:*)
 - Run Solution


### How to - Front End (using android):
 - InstalL Flutter SDK
 - Install Android SDK
 - Run "flutter doctor" in terminal to check for faults
 - Open front end project's directory in terminal and run "flutter run"

### How to - Access backend from mobile device
 - Connect hosting machine to mobile device's Hotspot
 - Change flutter's endpoints to IPV4 address of hosting Machine
 - Disable all firewalls in hosting Machine
 - Change Network to private type on hosting Machine
 
*If faced with any difficulties, contact with tautvydas.dirmeikis@mif.stud.vu.lt*

#### TEAM
 - Agota Kazėnaitė
 - Tautrimas Gumbys
 - Domantas Mitrius
 - Simonas Briedis
 - Tautvydas Dirmeiki