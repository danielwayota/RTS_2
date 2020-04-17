using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBus : MonoBehaviour
{
    protected static MessageBus _current;
    public static MessageBus current
    {
        get
        {
            if (_current == null)
            {
                var obj = new GameObject("MessageBus");
                _current = obj.AddComponent(typeof(MessageBus)) as MessageBus;

                _current.Init();
            }

            return _current;
        }
    }

    protected Dictionary<string, List<IMessageReceiver>> receivers;

    public void Init()
    {
        this.receivers = new Dictionary<string, List<IMessageReceiver>>();
    }

    public void AddReceiver(string messageName, IMessageReceiver receiver)
    {
        List<IMessageReceiver> receiverList;

        if (this.receivers.ContainsKey(messageName))
        {
            receiverList = this.receivers[messageName];
        }
        else
        {
            receiverList = new List<IMessageReceiver>();

            this.receivers.Add(messageName, receiverList);
        }

        receiverList.Add(receiver);
    }

    public void Send(Message msg)
    {
        if (this.receivers.ContainsKey(msg.name))
        {
            var receiverList = this.receivers[msg.name];

            foreach (var receiver in receiverList)
            {
                receiver.Receive(msg);
            }
        }
        else
        {
            Debug.LogError($"There is no receiver for message: {msg.name}");
        }
    }
}
