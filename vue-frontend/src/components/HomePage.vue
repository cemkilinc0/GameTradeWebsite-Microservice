<template>
  <div class="item-container">
    <h1>Item List</h1>
    <div v-if="items.length > 0" class="items-grid">
      <div class="item-card" v-for="item in items" :key="item.itemId">
        <img src= "/images/placeholder.png" alt="Item Placeholder" class="item-image"/>
        <div class="item-info">
          <h2 class="item-name">{{ item.itemName }}</h2>
          <p class="item-description">{{ item.description }}</p>
        </div>
      </div>
    </div>
    <p v-else>No items found.</p>
  </div>
</template>

<script>
import CatalogService from "@/services/CatalogService"

export default {
  name: 'HomePage',
  props: {
    msg: String
  },
  data() {
    return {
      items: [],  // Data property to store the items
    };
  },
  async created() {
    try {
      const response = await CatalogService.getAllItems(); // Assuming getItems() is your method
      this.items = response.data;
    } catch (error) {
      console.error(error);
      // Handle the error appropriately
    }
  }
}
</script>

<style>
/* Container for all items */
.item-container {
  max-width: 1200px;
  margin: auto;
  padding: 20px;
}

/* Grid layout for items */
.items-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
  gap: 20px;
}

/* Individual item card styling */
.item-card {
  background-color: #ffffff;
  border: 1px solid #dddddd;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  overflow: hidden;
  transition: transform 0.2s;
}

.item-card:hover {
  transform: translateY(-5px);
}

/* Image styling */
.item-image {
  width: 100%;
  height: 200px;
  object-fit: cover;
}

/* Item information styling */
.item-info {
  padding: 15px;
}

.item-name {
  margin-top: 0;
}

.item-description {
  color: #666666;
  font-size: 0.9em;
}
</style>
