const assert = require("assert");

module.exports = async function (fitzybot) {

    // given (arrange)
    const requestTxnId = "1235";

    const requestPayload = {
        "commandSource": "Twitch",
        "operation": "adjustPoints",
        "executedByUsername": "testExecutor",
        "targetUsername": "testTarget",
        "pointAdjustment": -750
    };

    const userKey = `${requestPayload.commandSource}-${requestPayload.targetUsername}`;

    // when (act)
    let actualPromise = fitzybot.adjustPoints(
        requestTxnId,     
        requestPayload
    );

    // assert    
    await assert.rejects(actualPromise, "Adjust Points Too Many Negative");
}