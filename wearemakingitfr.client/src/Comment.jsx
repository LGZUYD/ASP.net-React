import { useState, useEffect } from 'react';
function Comment({commentId, commentContent}) {
    //const [comments, setComments] = useState(0);

    return (
        <>
        <div className="commentContainer">
            <div>Comment Id: {commentId}</div>
                <div>Content: {commentContent}</div>
            </div>
        </>
    );
}

export default Comment;