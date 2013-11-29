require 'bson'
require 'mongo'

include Mongo

def last_week
	Time.now.utc - 7 * 60 * 60 * 24
end

# commented out for local development
#if not(defined?(params)) or params.nil?
#	params = {:uri => 'mongodb://nmoneys_web_local:hackMeNot@ds047968.mongolab.com:47968/nmoneys_web'}
#end
#puts "Payload: #{params}"

keys = Mongo::MongoClient.from_uri(params[:uri]).db().collection("keys")
keys.ensure_index({:Confirmed => Mongo::ASCENDING, :Requested => Mongo::ASCENDING})

last_week = last_week()
puts "Last week's date: #{last_week}"

query = {:query => {:Confirmed => nil, :Requested => {'$lte' => last_week}}}

pending_count = keys.count(query)
puts "Found [ #{pending_count} ] pending key requests older than '#{last_week}'"

if pending_count > 0
	keys.remove(query[:query])
	puts "Deleted [ #{pending_count} ] documents"
end



