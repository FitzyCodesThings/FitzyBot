'use strict'

module.exports = {
    // Dragonchain Client Instance 
    client: null,
    
    log: null,

    // Contract Methods //
    adjustPoints: async function(requestTxnId, payload)
    {
        // Get the user's current state //
        const userKey = `${payload.commandSource}-${payload.targetUsername}`;
        
        let userState = null; 
        
        try
        {
            userState = await this.getHeapObject({key: userKey});

        } catch (exception)
        {
            userState = {                
                "commandSource": payload.commandSource,
                "userName": payload.targetUsername,
                "balance": 0
            };   
        }            

        const pointAdjustment = payload.pointAdjustment;

        if (userState.balance + pointAdjustment < 0)
            throw "Insufficient points.";

        userState.balance += pointAdjustment;

        let output = {
            "response": {
                "commandSource": payload.commandSource,
                "operation": payload.operation,
                "executedByUsername": payload.executedByUsername,
                "targetUsername": payload.targetUsername,
                "pointAdjustment": payload.pointAdjustment,                
            },
            [userKey]: userState
        }

        return output;

    },


    // Helper Methods //
    getHeapObject: async function (options) {
        try {
            const objResponse = await this.client.getSmartContractObject({key: options.key})

            const obj = JSON.parse(objResponse.response);
            
            if (obj.error)
                throw "Object Not Found: " + obj.error.details;

            return obj;
        } catch (exception)
        {            
            throw exception
        }
    }
}