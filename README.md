# Flight Inspector Desktop Application
About the application
---------------------
This is a flight inspector desktop application. The application works as following:  
  
In the **Opening screen**, the user insetrs the .csv file with the flight data and the .xml file with the flight data settings, connects to the FlightGear application, which should be opened by the user, and inserts an anomaly detection algorithm (optional). Then, the application moves to the **Main screen**.    
  
![Screenshot](captures/welcome_window.png) 
  
The **Main screen** contains the following:  
**Scrollbar** that indicates the current time of the flight video, which is controlled by the user. The user can move the scrollbar to the exact time point that he wants to make 
the application show the data of that time point. From the scrollbar, the user can pause, play and increase the speed of the flight video.  
The red pointes indicate the time where an anomaly has happaned.  
  
![Screenshot](captures/scrollbar_window.png)  
  
**Statistics window** that shows some important data like altimeter, airspeed and more.  
**Graphs window** that shows 3 graphs about the data property that the user has chosen:
Graph of that data property, graph of the data property with is most corelative to the current data property, and graph that shows the linear regression between those two data properties.  
**Rudder window** that contains the joystick and the rudder and throttle measures.  
  
![Screenshot](captures/stuff_window.png)  
  
**The FlightGear application shows the flight video, while the flight inspector application shows and analyzes the data.**  

  
![Screenshot](captures/fg_window.jpeg)  
  
  
About the project structure
---------------------------
The project implements the **MVVM architectural pattern**. Accordingly, the project files are splitted into three folders - **Model**, **View**, and **ViewModel**.  
The responsility **Model** is to analyze the given flight data .csv file, communicate with the *FlightGear*, and do all of the calculations like finding linear regression between two flight data properties.  
The **View** is responsibe on showing the flight data to the user in an interactive way - data like the graphs and the anomaly. The View is also responsible on getting commands from the user - commands like pause the rendering and increase the rendering speed.  
The **ViewModel** is responsible on connecting between the **Model** and the **View**, while making an *abstraction* of the **Model** for the **View**.  
  
About the directories:  
***Model directory*** which contains the class that are related to the model. *FlightGearModel* is the facade model class, which implements the *IFlightGearModel* interface.  
This class contains an instance of an object that implements the *IAsyncFGClient* interface, and uses that object to communicate with the FlightGear asynchronously.  
In this way, the *IFlightGearModel* and *IAsyncFGClient* interfaces are built together in the *Strategy design pattern*, which allows you to combine a *FlightGearModel* with every *AsyncFGClient* which implements the *IAsyncFGClient* interface. In the project, we implemented the *AsyncTcpFGClient*, which helps the *FlightGearModel* to communicate with the *FlightGear* in TCP protocol.  
***ViewModel directory*** which contains a ViewModel for every user control, for example - the *PlayBackViewModel* is a ViewModel for the playback scrollbar, which gets from the 
model the data which is relevant to the scrollbar (like the flight video speed), and decorates the relevant operations of the model for the scrollbar, like *Render* which starts 
the rendering of the flight data. There is a data binding between every ViewModel to its user control.  
***View directory*** which contains the .xaml file and the C# code behind for every user controls and window. The directory contains 2 windows - *Opening window*, and *Main window*, and all of the user controls which exist in those windows.  
**Plugins directory** which contains two anomaly algorithm plugins.  

Required installations
----------------------
The application is written in **WPF .NETCORE 3.0**.  
To run the application you should have the following:  
**.NETCORE 3.0 environment**. The *.NETCORE* version should be at least 3.0, because this is the first version which supports *WPF*.  
**OxyPlot plugin for WPF**. *OxyPlot* plugin is used to produce and show the graphs of the flight data.  
**FlightGear application**. The *FlightGear* application is used to show a video of the flight. We recommand to use the last version of the *FlightGear*, which can be downloaded in the link **www.flightgear.org**.  

Installation and Running instructions
-------------------------------------
There are **many ways** to run the application. The easiest is to run it from **Visual Studio**, but it is important to know that there are **other options**, like compiling  
the code manualy and run in a *.NETCORE 3.0* environment. Notice that the *OxyPlot* plugin is requird to run the applicaiton.  
  
*How to run the application in Visual Studio ?*  
**First**, install the Visual Studio and **clone this repository**.  
**Second**, install the *OxyPlot* plugin. In Visual Studio: **Project -> Manage NuGet Packages -> Browse -> Search "OxyPlot Wpf" -> Install OxyPlot.Wpf**  
**Then**, run the application by pressing the green run button in the Visual Studio. The *Opening window* will be opened, and the instructions will be showed there. Then open the *FlightGear* application and do the instructions which are showed in our application.  

About the plugin
----------------
The application supports inserting anomaly detection algorithm plugin dynamically. In the *Plugins* directory, there are two anomaly detection algorithm plugins that you can insert to the application. Those plugins are written in C++.  

**To use your own plugin**, your plugin should contain a function with the signature:  
**Detect(StringBuilder str, int len, string normalDataFilePath, string anomalyDataFilePath)**  
***str*** - a string builder that will finally hold the string that represents the anomaly. This string format will be as following:  
Every line of the string will be in the pattern:  
*FrameNumber:FirstAnomalyProperty1-SecondAnomalyProperty1,FirstAnomalyProperty2-SecondAnomalyProperty2,...,*  
where *FirstAnomalyProperty* and *SecondAnomalyProperty* are the indices of the corelative anomaly properties.  
***len*** - the length of the string that the parameter ***str*** represents.  
***normalDataFilePath*** - the path to the .csv file with the normal flight data.  
***anomalyDataFilePath*** - the path to the .csv file with the anomaly flight data.  

**Note** that the anomaly detection algorithm plugin can be written in every language that can implement the function with the signature above, like C++, C#, and Java, but we tested the application only with plugins that were written in C++.  

Demonstration Video
----------------
https://youtu.be/2U8jjl2nS5c




