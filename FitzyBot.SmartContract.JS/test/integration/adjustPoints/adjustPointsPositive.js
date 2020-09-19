const assert = require("assert");

module.exports = async function (fitzybot) {

    // given (arrange)
    const requestTxnId = "1234";

    const requestPayload = {
        "commandSource": "Twitch",
        "operation": "adjustPoints",
        "executedByUsername": "testExecutor",
        "targetUsername": "testTarget",
        "pointAdjustment": 1000
    };

    const userKey = `${requestPayload.commandSource}-${requestPayload.targetUsername}`;

    const expectedResponse = {
        "response": {
            "commandSource": "Twitch",
            "operation": "adjustPoints",
            "executedByUsername": "testExecutor",
            "targetUsername": "testTarget",
            "pointAdjustment": 1000
        },
        [userKey]: {
            "commandSource": "Twitch",
            "userName": "testTarget",
            "balance": 1000
        }   
    };

    // when (act)
    const actualResponse = await fitzybot.adjustPoints(
        requestTxnId,     
        requestPayload
    );

    fitzybot.client.updateSmartContractHeap(actualResponse);

    // assert    
    assert.deepStrictEqual(actualResponse, expectedResponse, "Adjust Points Positive");
    
}