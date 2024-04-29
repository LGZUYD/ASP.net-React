import { useState, useEffect } from 'react';
import './Message.css'

function Message() {
    const [messages, setMessages] = useState([]);

    useEffect(() => {
        fetchMessages();
    }, []); // empty array hier, weet niet precies waarom

    const fetchMessages = () => {
        fetch('https://localhost:7247/messages')
            .then(response => response.json())
            .then(data => setMessages(data))
            .catch(error => console.log(error));
    };

    const deleteMessage = (id) => {
        fetch(`https://localhost:7247/messages/${id}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(() => fetchMessages()) 
            .catch(error => console.log(error));
    };

    const updateMessage = (event) => { // dit werkt niet
        event.preventDefault();
        const form = event.target;
        const formData = new FormData(form);

        fetch(`https://localhost:7247/messages/${formData.get('messageId')}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                messageId: formData.get('messageId'),
                messageContent: formData.get('inputField')
            })
        })
            .then(() => fetchMessages())
            .catch(error => console.log(error));
    };

    return (
        <div>
            {messages.map((message) => (
                <div className="message" key={message.messageId}>
                    <li>
                        Message ID: {message.messageId} <br />
                        Content: <br /> {message.messageContent}
                        <button onClick={() => deleteMessage(message.messageId)}>Delete</button>
                        <form onSubmit={updateMessage}>
                            <label>
                                Update message
                                <input
                                    type="text"
                                    name="inputField"
                                />
                                <input type="hidden" name="messageId" value={message.messageId} />
                            </label>
                            <button type="submit">Submit</button>
                        </form>
                    </li>
                </div>
            ))}
        </div>
    );
}

export default Message;
