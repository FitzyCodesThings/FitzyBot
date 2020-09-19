const sdk = require("dragonchain-sdk");
const fitzybot = require("./fitzybot");

const log = console.error; // Remember that STDERR is the logging stream.

module.exports = async inputObj => {
  try {
    fitzybot.client = await sdk.createClient();
    fitzybot.log = log;

    let output = await Reflect.apply(
      fitzybot[inputObj.payload.operation],
      fitzybot,
      [
        inputObj.header.txn_id,
        inputObj.payload
      ]
    );

    return output;
        
  } catch (exception)    
  {
      // Write the exception to STDERR
      log(exception);

      return {"exception": exception};  
  }
};