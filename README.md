# DistroLocale
Final Project for CSM CSCI565 - Distributed Computing

## Running

Initialize a `roscore`, a `rosbridge`, the unity simulation, two `gmappers`, and ???


### Roscore

Nothing you haven't seen before.

    roscore

### Rosbridge

Bridge server between Unity (ROS# Websockets) and ROS

    roslaunch rosbridge_server rosbridge_websocket.launch

### Unity

Hit Play.

### Gmappers

For each robot, you need to export a namespace for gmapper to properly create local maps. 

    export ROS_NAMESPACE=/robot1

    rosrun gmapping slam_gmapping scan:=/robot1/scan _odom_frame:=robot1_odom _base_frame:=map _map_update_interval:=1.0 _angularUpdate:=0.1 _linearUpdate:=0.1


