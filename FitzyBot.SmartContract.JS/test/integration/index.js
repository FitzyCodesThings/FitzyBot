const fs = require('fs');
const assert = require("assert");
const fitzybot = require("../../src/contract/fitzybot");

const test = {
    adjustPoints: {
        adjustPointsPositive: require("./adjustPoints/adjustPointsPositive"),
        adjustPointsNegative: require("./adjustPoints/adjustPointsNegative"),
        adjustPointsTooManyNegative: require("./adjustPoints/adjustPointsTooManyNegative")
    }
};

fitzybot.client = {
    heap: {},

    updateSmartContractHeap: function (data) {
        this.heap = {...this.heap, ...data};

        // Write current heap to file //
        fs.writeFileSync(__dirname + '/post-run-heap.json', JSON.stringify(this.heap, null, 2), (err) => {    
            if (err) throw err;
        });
    },

    getSmartContractObject: async function (options) {

        if (this.heap[options.key])
        {
            return {
                "status": 200,
                "response": JSON.stringify(this.heap[options.key]),
                "ok": true
            }
        }

        return {
            "status": 404,
            "response": JSON.stringify({"error":{"type":"NOT_FOUND","details":"The requested resource(s) cannot be found."}}),
            "ok": false
          };          
    }
};

(async () => {

    try 
    {
        // +++ Adjust Points Tests +++ //
        await test.adjustPoints.adjustPointsPositive(fitzybot);
        await test.adjustPoints.adjustPointsNegative(fitzybot);
        await test.adjustPoints.adjustPointsTooManyNegative(fitzybot);


        // +++ ISSUER TESTS +++ //

        // Assert creating issuer with missing required data fails //
        //await assert.rejects(test.issuer.create_issuer_missing_data(badger), "Create Issuer: Missing Data");

        // Assert issuer created //
        // result = await test.issuer.create_issuer(badger);

        // assert.deepStrictEqual(result.actual, result.expected, "Create Issuer");

        // let issuer = await badger.getHeapObject({"key": `issuer-${result.requestTxnId}`});

        console.log("Tests passed!");
    }    
    catch (exception) 
    {
        console.error(exception);

        console.error("Test(s) failed.");
    }

    


})();