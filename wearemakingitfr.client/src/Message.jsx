import { useState, useEffect } from 'react';
import './Message.css'
import Comment from "./Comment.jsx";

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

    const createMessage = (event) => {

        event.preventDefault();
        const form = event.target;
        const formData = new FormData(form);

        fetch('https://localhost:7247/messages', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ MessageContent: formData.get('createMessage') })
        })
            .then(() => fetchMessages())
            .catch(error => {
                console.error('Error sending data:', error);
            });

    }

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

    const createComment = (event, messageId) => {
        event.preventDefault();
        const form = event.target;
        const formData = new FormData(form);

        fetch(`https://localhost:7247/comments/${messageId}`, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ messageId: messageId, commentContent: formData.get('inputField-comment') })
        })
            .then(() => fetchMessages()) // Update messages after creating comment
            .catch(error => {
                console.error('Error sending data:', error);
            });
    }


    const viewComments = (messageId) => {
        console.log(messageId);
    }

    const updateMessage = (event) => {
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
        <>
            <form onSubmit={createMessage}>
                <label>
                    Create new message
                    <input
                        type="text"
                        name="createMessage"
                    />
                </label>
                <button type="submit">Submit</button>
            </form>
            <div className="container">
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
                           
                            <div className="allCommentsContainer">
                                {message.allComments.length > 0 && 
                                    message.allComments.map((comment) => (
                                        <Comment key= { comment.commentId } commentId={comment.commentId} commentContent={comment.commentContent} />
                                    ))
                                }
                            </div>

                            <form onSubmit={(event) => createComment(event, message.messageId)}>
                                <label>
                                    create comment
                                    <input
                                        type="text"
                                        name="inputField-comment"
                                    />
                                    <input type="hidden" name="messageIdCommentCreate" value={message.messageId} />
                                </label>
                                <button type="submit">Submit</button>
                            </form>

                        </li>
                    </div>
                ))}
            </div>
        </>
    );

}

export default Message;
