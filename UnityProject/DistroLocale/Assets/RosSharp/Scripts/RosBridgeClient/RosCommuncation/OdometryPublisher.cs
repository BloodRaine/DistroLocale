using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class OdometryPublisher : UnityPublisher<MessageTypes.Nav.Odometry>
    {
        public Transform PublishedTransform;
        public string FrameId = "Unity";
        public string ChildFrameId = "UnityBase";

        private MessageTypes.Nav.Odometry message;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void FixedUpdate()
        {
            UpdateMessage();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Nav.Odometry
            {
                header = new MessageTypes.Std.Header()
                {
                    frame_id = FrameId
                },
                child_frame_id = ChildFrameId
            };
        }

        private void UpdateMessage()
        {
            message.header.Update();
            SetGeometryPoint(PublishedTransform.position.Unity2Ros(), message.pose.pose.position);
            SetGeometryQuaternion(PublishedTransform.rotation.Unity2Ros(), message.pose.pose.orientation);

            Publish(message);
        }

        private static void SetGeometryPoint(Vector3 position, MessageTypes.Geometry.Point message)
        {
            message.x = position.x;
            message.y = position.y;
            message.z = position.z;
        }

        private static void SetGeometryQuaternion(Quaternion quaternion, MessageTypes.Geometry.Quaternion message)
        {
            message.x = quaternion.x;
            message.y = quaternion.y;
            message.z = quaternion.z;
            message.w = quaternion.w;
        }

    }
}
