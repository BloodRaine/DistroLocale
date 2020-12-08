using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class TFPublisher : UnityPublisher<MessageTypes.Tf2.TFMessage>
    {
        public Transform WorldPosition;
        public Transform Robot1Position;
        public Transform Robot2Position;


        public string WorldFrameID = "world";
        public string Robot1FrameID = "robot1_base";
        public string Robot2FrameID = "robot2_base";

        private MessageTypes.Tf2.TFMessage tf_message;
        private MessageTypes.Geometry.TransformStamped tf1;
        private MessageTypes.Geometry.TransformStamped tf2;

        MessageTypes.Geometry.TransformStamped[] v1;

        protected override void Start()
        {
            v1 = new MessageTypes.Geometry.TransformStamped[2];

            base.Start();
            InitializeMessage();
        }

        private void FixedUpdate()
        {
            UpdateMessage();
        }

        private void InitializeMessage()
        {
            tf1 = new MessageTypes.Geometry.TransformStamped
            {
                header = new MessageTypes.Std.Header()
                {
                    // stamp = Time.time,
                    frame_id = WorldFrameID
                },
                child_frame_id = Robot1FrameID
            };
            tf2 = new MessageTypes.Geometry.TransformStamped
            {
                header = new MessageTypes.Std.Header()
                {
                    // stamp = Time.time,
                    frame_id = WorldFrameID
                },
                child_frame_id = Robot2FrameID
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
            tf1.header.Update();
            GetGeometryPoint(Robot1Position.position.Unity2Ros(), WorldPosition.position.Unity2Ros(), Robot1Position.rotation.Unity2Ros(), tf1.transform.translation);
            GetGeometryQuaternion(Robot1Position.rotation.Unity2Ros(), WorldPosition.rotation.Unity2Ros(), tf1.transform.rotation);
            tf2.header.Update();
            GetGeometryPoint(Robot2Position.position.Unity2Ros(), WorldPosition.position.Unity2Ros(), Robot2Position.rotation.Unity2Ros(), tf2.transform.translation);
            GetGeometryQuaternion(Robot2Position.rotation.Unity2Ros(), WorldPosition.rotation.Unity2Ros(), tf2.transform.rotation);

            v1[0] = tf1;
            v1[1] = tf2;
            tf_message = new MessageTypes.Tf2.TFMessage(v1);

            Publish(tf_message);
        }

        private static void GetGeometryPoint(Vector3 position, Vector3 origin, Quaternion rotation, MessageTypes.Geometry.Vector3 translation)
        {
            //Quaternion reverse = Quaternion.Inverse(rotation);
            //Vector3 newposition = reverse * position;
            translation.x = (origin.x + position.x);
            translation.y = (origin.y + position.y);
            translation.z = 0; // no vertical translation
        }

        private static void GetGeometryQuaternion(Quaternion quaternion, Quaternion origin_quaternion, MessageTypes.Geometry.Quaternion geometryQuaternion)
        {
            // We don't do anything with the origin quaternion right now
            // Quaternion reverse = Quaternion.Inverse(quaternion);
            geometryQuaternion.x = quaternion.x;
            geometryQuaternion.y = quaternion.y;
            geometryQuaternion.z = quaternion.z;
            geometryQuaternion.w = quaternion.w;
        }

    }
}
