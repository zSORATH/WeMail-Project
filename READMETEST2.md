# README

## ROS
This project uses the ROS (Robot Operating System) as a framework. If you do not allready have ROS installed, then
follow this link [ROS installation guide](http://wiki.ros.org/melodic/Installation/Ubuntu) and install the newest version of the `ros-melodic` framework for Ubuntu 18.04. 

## Dependencies 
This project depends on source files from the manufacturer of the turtlebot3 model. If you don't allready have these
packages installed and configured with your ROS environment, then download them by typing these commands:

-  `$ cd (path to your catkin workspace)`
-  `$ git clone https://github.com/ROBOTIS-GIT/turtlebot3_msgs.git`
-  `$ git clone -b melodic-devel https://github.com/ROBOTIS-GIT/turtlebot3.git`
-  `$ git clone https://github.com/ROBOTIS-GIT/turtlebot3_simulations.git`

At the root of your catkin workspace run the following command, to install the packages:

-  `$ catkin_make`

## Instructions on how to run this project

Download this repository and copy the `turtlebot_projekt_pkg` folder to your catkin workspace:

-  `$ git clone https://github.com/Kristoffer-PBS/Computer-project-1.git`

-  `$ cp -r Computer-project-1/turtlebot_project_pkg/ (path to your catkin workspace)`

Add executable permission to the python file:

-  `$ cd (path to your catkin workspace)`

-  `$ chmod +x turtlebot_project_pkg/turtlebot.py`

-  `$ catkin_make`

To use ROS cli tools you have to source the setup file specific for your shell (e.g. `.bash` or `.zsh`) of your catkin workspace:

-  `$ source devel/setup.bash`

-  `$ source devel/setup.zsh`

There are three different turtlebot3 models, this project uses the burger model. You can either export it in your local 
terminal session by using the command:

-  `$ export TURTLEBOT3_MODEL=burger`

or if you want the variable configuration to persist between terminal sessions, then add it to your shell configuration file (e.g. `~/.bashrc` or `~/.zshrc`):

-  `$ echo "export TURTLEBOT3_MODEL=burger" >> ~/.bashrc`

-  `$ echo "export TURTLEBOT3_MODEL=burger" >> ~/.zshrc`

Launch your Gazebo simulation:

-  `$ roslaunch turtlebot3_gazebo turtlebot3_world.launch`

Launch the SLAM gmapping module:

-  `$ roslaunch turtlebot3_slam turtlebot3_slam.launch slam_methods:=gmapping`

Run the navigation algorithm:

-  `$ rosrun turtlebot_project_pkg turtlebot.py`

