const assert = require("assert");

module.exports = async function (fitzybot) {

    // given (arrange)
    const requestTxnId = "1235";

    const requestPayload = {
        "commandSource": "Twitch",
        "operation": "adjustPoints",
        "executedByUsername": "testExecutor",
        "targetUsername": "testTarget",
        "pointAdjustment": -500
    };

    const userKey = `${requestPayload.commandSource}-${requestPayload.targetUsername}`;

    const expectedResponse = {
        "response": {
            "commandSource": "Twitch",
            "operation": "adjustPoints",
            "executedByUsername": "testExecutor",
            "targetUsername": "testTarget",
            "pointAdjustment": -500
        },
        [userKey]: {
            "commandSource": "Twitch",
            "userName": "testTarget",
            "balance": 500
        }   
    };

    // when (act)
    const actualResponse = await fitzybot.adjustPoints(
        requestTxnId,     
        requestPayload
    );

    fitzybot.client.updateSmartContractHeap(actualResponse);

    // assert    
    assert.deepStrictEqual(actualResponse, expectedResponse, "Adjust Points Negative");
    
}