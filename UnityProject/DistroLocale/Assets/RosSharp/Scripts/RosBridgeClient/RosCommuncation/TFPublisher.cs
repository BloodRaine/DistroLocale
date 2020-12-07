using UnityEngine;

namespace RosSharp.RosBridgeClient
{
   public class TFPublisher : UnityPublisher<MessageTypes.Tf2.TFMessage>
   {
       public Transform CurrentPosition;


       public string WorldFrameID = "world";
       public string ChildFrameID = "robot_base";
        
       private MessageTypes.Tf2.TFMessage tf_message;
       private MessageTypes.Geometry.TransformStamped tfs;

       MessageTypes.Geometry.TransformStamped[] v1;

       protected override void Start()
       {
           v1 = new MessageTypes.Geometry.TransformStamped[1];

           base.Start();
           InitializeMessage();
       }

       private void FixedUpdate()
       {
           UpdateMessage();
       }

       private void InitializeMessage()
       {
            tfs = new MessageTypes.Geometry.TransformStamped {
                header = new MessageTypes.Std.Header() {
                    // stamp = Time.time,
                    frame_id = WorldFrameID
                    // child_frame_id = ChildFrameID
                }
            };
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
       }

       private void UpdateMessage()
       {
        //    tfs = GetTransformStamped();
            tfs.header.Update();
            GetGeometryPoint(CurrentPosition.position.Unity2Ros(), tfs.transform.translation);
            GetGeometryQuaternion(CurrentPosition.rotation.Unity2Ros(), tfs.transform.rotation);

            v1[0] = tfs;
            tf_message = new MessageTypes.Tf2.TFMessage(v1);

            Publish(tf_message);
       }

       private static void GetGeometryPoint(Vector3 position, MessageTypes.Geometry.Vector3 translation)
       {
           translation.x = position.x;
           translation.y = position.y;
           translation.z = 0; // no vertical translation
       }

       private static void GetGeometryQuaternion(Quaternion quaternion, MessageTypes.Geometry.Quaternion geometryQuaternion)
       {
           geometryQuaternion.x = quaternion.x;
           geometryQuaternion.y = quaternion.y;
           geometryQuaternion.z = quaternion.z;
           geometryQuaternion.w = quaternion.w;
       }

   }
}
