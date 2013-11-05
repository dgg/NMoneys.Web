require 'bson'
require 'mongo'

include Mongo

uri = "mongodb://nmoneys_web_local:hackMeNot@ds047968.mongolab.com:47968/nmoneys_web"
client = Mongo::MongoClient.from_uri(uri)
db = client.db()
heroku = db.collection("heroku")
doc = {"name" => "MongoDB", "type" => "database", "count" => 1, "info" => {"x" => 203, "y" => '102'}}
heroku.insert(doc)