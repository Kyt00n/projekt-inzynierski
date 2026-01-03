import SearchBar from "@/components/searchBar";
import { ActivityIndicator, FlatList, ScrollView, Text, View } from "react-native";
import { useRouter } from "expo-router";
import useFetch from "@/services/useFetch";
import { getUserOrders } from "@/services/api";
import PostCard from "@/components/postCard";
import { Order } from "@/interfaces/Order";
import { useAuth } from "../authProvider";
import OrderCard from "@/components/orderCard";

export default function Index() {
  const router = useRouter();
  const {authState} = useAuth();
  const {
    data: orders,
    loading,
    error,
    refetch,
  } = useFetch<Order[]>(() => getUserOrders(authState?.userId as string));
  
  const renderItem = ({ item }: { item: Order }) => (
    <View className="items-center mb-4">
      <OrderCard {...item} />
    </View>
  )

  return (
    <View className="flex-1 bg-primary">
          <View className="flex-1 px-5" style={{ paddingTop: 60 }}>
            <Text className="w-12 h-10 mb-4 mx-auto text-center text-4xl">🚚</Text>
            <Text className="mx-auto text-center text-3xl text-white font-bold">Your Trips</Text>
    
            {loading ? (
              <ActivityIndicator size="large" color="#0000ff" className="mt-10 self-center" />
            ) : error ? (
              <View className="mt-6 items-center">
                <Text className="text-red-400 text-sm mb-2">{error.message}</Text>
                <Text className="text-white underline" onPress={refetch}>
                  Retry
                </Text>
              </View>
            ) : (
              <FlatList
                data={orders ?? []}
                keyExtractor={(item) => item.orderId}
                renderItem={renderItem}
                contentContainerStyle={{ paddingBottom: 32, paddingTop: 20 }}
                showsVerticalScrollIndicator={true}
                refreshing={loading}
                onRefresh={refetch}
                ListEmptyComponent={() => (
                  <View className="mt-10 items-center px-4">
                    <Text className="text-gray-400 text-center">
                      You have no trips, go to active orders to browse through newest offers
                    </Text>
                    <Text
                      className="text-white mt-3 underline"
                      onPress={() => router.push('/activeOrders')}
                    >
                      Browse active orders
                    </Text>
                  </View>
                )}
              />
            )}
          </View>
        </View>
  );
}
