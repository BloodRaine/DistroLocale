//using UnityEngine;

//namespace RosSharp.RosBridgeClient
//{
//    public class TFPublisher : UnityPublisher<MessageTypes.Tf2.TFMessage>
//    {
//        public Transform PublishedTransform;


//        public string WorldFrameID = "world";
//        public string ChildFrameId = "robot_base";
        
//        private MessageTypes.Geometry.TransformStamped tf_message;

//        protected override void Start()
//        {
//            base.Start();
//            InitializeMessage();
//        }

//        private void FixedUpdate()
//        {
//            UpdateMessage();
//        }

//        private void InitializeMessage()
//        {
//            message = new MessageTypes.Geometry.PoseStamped
//            {
//                header = new MessageTypes.Std.Header()
//                {
//                    frame_id = WorldFrameID
//                }
//            };
//            if (PublishTF)
//            {
//                tf_message = new MessageTypes.Geometry.TransformStamped
//                {
//                    header = new MessageTypes.Std.Header()
//                    {
//                        frame_id = WorldFrameID
//                    },
//                    child_frame_id = ChildFrameId
//                };
//            }
//        }

//        private void UpdateMessage()
//        {
//            message.header.Update();
//            GetGeometryPoint(PublishedTransform.position.Unity2Ros(), message.pose.position);
//            GetGeometryQuaternion(PublishedTransform.rotation.Unity2Ros(), message.pose.orientation);

//            Publish(message);
//        }

//        private static void GetGeometryPoint(Vector3 position, MessageTypes.Geometry.Point geometryPoint)
//        {
//            geometryPoint.x = position.x;
//            geometryPoint.y = position.y;
//            geometryPoint.z = position.z;
//        }

//        private static void GetGeometryQuaternion(Quaternion quaternion, MessageTypes.Geometry.Quaternion geometryQuaternion)
//        {
//            geometryQuaternion.x = quaternion.x;
//            geometryQuaternion.y = quaternion.y;
//            geometryQuaternion.z = quaternion.z;
//            geometryQuaternion.w = quaternion.w;
//        }

//    }
//}
